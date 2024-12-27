namespace PokemonGame;

public class Pokemon
{
    // Propriétés principales
    public string Name { get; set; }
    public int Level { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int SpecialAttack { get; set; }
    public int SpecialDefense { get; set; }
    public int Speed { get; set; }
    public int Experience { get; set; }
    public string Type1 { get; set; }
    public string? Type2 { get; set; }

    // Liste des attaques
    public List<AttackLogic> Moves { get; set; } = new();

    // Générateur aléatoire pour divers calculs
    private static readonly Random RandomInstance = new();

    // Constructeur
    public Pokemon(string name, int level, int health, int attack, string type1, string? type2 = null)
    {
        Name = name;
        Level = level;
        Health = health;
        Attack = attack;
        Defense = level * 2;
        SpecialAttack = level * 3; // Exemple
        SpecialDefense = level * 2; // Exemple
        Speed = level * 3; // Exemple
        Experience = 0;
        Type1 = type1;
        Type2 = type2;
    }

    // Ajouter une attaque
    public void LearnMove(AttackLogic move)
    {
        if (Moves.Count >= 4)
        {
            Console.WriteLine($"{Name} ne peut pas apprendre plus de 4 attaques !");
            return;
        }
        Moves.Add(move);
        Console.WriteLine($"{Name} apprend {move.Name} !");
    }

    // Oublier une attaque
    public void ForgetMove(string moveName)
    {
        AttackLogic? move = Moves.FirstOrDefault(m => m.Name == moveName);
        if (move != null)
        {
            Moves.Remove(move);
            Console.WriteLine($"{Name} oublie {moveName} !");
        }
        else
        {
            Console.WriteLine($"{Name} ne connaît pas {moveName} !");
        }
    }

    // Utiliser une attaque
    public void UseMove(string moveName, Pokemon target)
    {
        AttackLogic? move = Moves.FirstOrDefault(m => m.Name == moveName);
        if (move == null)
        {
            Console.WriteLine($"{Name} ne connaît pas {moveName} !");
            return;
        }
        move.Use(this, target);
    }

    // Vérifier si le Pokémon est KO
    public bool IsFainted() => Health <= 0;

    // Gagner de l'expérience
    public void GainExperience(int exp)
    {
        Experience += exp;
        while (Experience >= Level * 5)
        {
            Experience -= Level * 5;
            LevelUp();
        }
    }

    // Monter de niveau
    private void LevelUp()
    {
        Level++;
        Health += 10;
        Attack += 4;
        Defense += 3;
        SpecialAttack += 3;
        SpecialDefense += 3;
        Speed += 2;
        Console.WriteLine($"{Name} monte au niveau {Level} !");
    }

    // Soigner les PV
    public void Heal()
    {
        int maxHealth = Level * 12;
        Health = maxHealth;
        Console.WriteLine($"{Name} récupère tous ses PV (PV max : {maxHealth}) !");
    }

    // Combattre un autre Pokémon
    public void AttackPokemon(Pokemon target, AttackLogic move)
    {
        Console.WriteLine($"{Name} utilise {move.Name} !");

        // Multiplier pour les deux types
        double multiplier1 = GetTypeMultiplier(move.Type, target.Type1);
        double multiplier2 = target.Type2 != null ? GetTypeMultiplier(move.Type, target.Type2) : 1.0;

        double totalMultiplier = multiplier1 * multiplier2;

        string effectiveness = GetEffectivenessDescription(totalMultiplier);

        // Calcul des dégâts
        int damage = (int)((move.Category == "Physique" ? Attack : SpecialAttack - target.Defense / 2.0) * totalMultiplier);
        damage = Math.Max(1, damage); // Les dégâts doivent être au minimum de 1
        target.Health -= damage;

        Console.WriteLine($"{target.Name} perd {damage} PV ! {effectiveness}");
    }


    // Méthode améliorée pour le calcul du multiplicateur de type
    public static double GetTypeMultiplier(string attackerType, string targetType)
    {
        Dictionary<string, Dictionary<string, double>> typeAdvantages = new()
    {
        { "Normal", new()
            {
                { "Roche", 0.5 }, { "Acier", 0.5 }, { "Spectre", 0.0 },
                { "Feu", 1.0 }, { "Eau", 1.0 }, { "Plante", 1.0 }, { "Électrique", 1.0 },
                { "Glace", 1.0 }, { "Combat", 1.0 }, { "Poison", 1.0 }, { "Sol", 1.0 },
                { "Vol", 1.0 }, { "Psy", 1.0 }, { "Insecte", 1.0 }, { "Dragon", 1.0 },
                { "Ténèbres", 1.0 }, { "Fée", 1.0 }
            }},
        { "Feu", new()
            {
                { "Plante", 2.0 }, { "Glace", 2.0 }, { "Insecte", 2.0 }, { "Acier", 2.0 },
                { "Eau", 0.5 }, { "Roche", 0.5 }, { "Dragon", 0.5 }, { "Feu", 0.5 },
                { "Électrique", 1.0 }, { "Combat", 1.0 }, { "Poison", 1.0 }, { "Sol", 1.0 },
                { "Vol", 1.0 }, { "Psy", 1.0 }, { "Spectre", 1.0 }, { "Ténèbres", 1.0 },
                { "Fée", 1.0 }
            }},
        { "Eau", new()
            {
                { "Feu", 2.0 }, { "Roche", 2.0 }, { "Sol", 2.0 },
                { "Eau", 0.5 }, { "Plante", 0.5 }, { "Dragon", 0.5 },
                { "Électrique", 1.0 }, { "Combat", 1.0 }, { "Poison", 1.0 },
                { "Vol", 1.0 }, { "Psy", 1.0 }, { "Spectre", 1.0 }, { "Insecte", 1.0 },
                { "Ténèbres", 1.0 }, { "Fée", 1.0 }
            }},
        { "Plante", new()
            {
                { "Eau", 2.0 }, { "Roche", 2.0 }, { "Sol", 2.0 },
                { "Feu", 0.5 }, { "Plante", 0.5 }, { "Insecte", 0.5 },
                { "Dragon", 0.5 }, { "Vol", 0.5 },
                { "Électrique", 1.0 }, { "Combat", 1.0 }, { "Poison", 1.0 },
                { "Spectre", 1.0 }, { "Glace", 1.0 }, { "Ténèbres", 1.0 },
                { "Fée", 1.0 }
            }},
        { "Électrique", new()
            {
                { "Eau", 2.0 }, { "Vol", 2.0 },
                { "Plante", 0.5 }, { "Dragon", 0.5 }, { "Électrique", 0.5 },
                { "Sol", 0.0 },
                { "Feu", 1.0 }, { "Glace", 1.0 }, { "Combat", 1.0 },
                { "Poison", 1.0 }, { "Spectre", 1.0 }, { "Ténèbres", 1.0 },
                { "Fée", 1.0 }
            }},
        { "Glace", new()
            {
                { "Plante", 2.0 }, { "Sol", 2.0 }, { "Dragon", 2.0 }, { "Vol", 2.0 },
                { "Eau", 0.5 }, { "Feu", 0.5 }, { "Glace", 0.5 }, { "Acier", 0.5 },
                { "Électrique", 1.0 }, { "Combat", 1.0 }, { "Poison", 1.0 },
                { "Spectre", 1.0 }, { "Ténèbres", 1.0 }, { "Fée", 1.0 }
            }},
        { "Combat", new()
            {
                { "Normal", 2.0 }, { "Roche", 2.0 }, { "Glace", 2.0 }, { "Ténèbres", 2.0 },
                { "Acier", 2.0 },
                { "Psy", 0.5 }, { "Vol", 0.5 }, { "Fée", 0.5 }, { "Spectre", 0.0 },
                { "Feu", 1.0 }, { "Eau", 1.0 }, { "Plante", 1.0 },
                { "Électrique", 1.0 }, { "Insecte", 1.0 }, { "Dragon", 1.0 }
            }},
        { "Poison", new()
            {
                { "Plante", 2.0 }, { "Fée", 2.0 },
                { "Roche", 0.5 }, { "Sol", 0.5 }, { "Spectre", 0.5 },
                { "Poison", 0.5 }, { "Acier", 0.0 },
                { "Feu", 1.0 }, { "Eau", 1.0 }, { "Électrique", 1.0 },
                { "Combat", 1.0 }, { "Insecte", 1.0 }, { "Vol", 1.0 },
                { "Psy", 1.0 }, { "Ténèbres", 1.0 }
            }},
        { "Sol", new()
            {
                { "Feu", 2.0 }, { "Électrique", 2.0 }, { "Roche", 2.0 },
                { "Acier", 2.0 }, { "Plante", 0.5 }, { "Insecte", 0.5 },
                { "Vol", 0.0 },
                { "Eau", 1.0 }, { "Psy", 1.0 }, { "Spectre", 1.0 }, { "Ténèbres", 1.0 },
                { "Fée", 1.0 }
            }},
        { "Vol", new()
            {
                { "Plante", 2.0 }, { "Combat", 2.0 }, { "Insecte", 2.0 },
                { "Électrique", 0.5 }, { "Roche", 0.5 }, { "Acier", 0.5 },
                { "Feu", 1.0 }, { "Eau", 1.0 }, { "Glace", 1.0 }, { "Dragon", 1.0 }
            }},
        // Complétez les autres types ici...
    };

        if (!typeAdvantages.ContainsKey(attackerType))
        {
            Console.WriteLine($"[ERREUR] Type attaquant inconnu : {attackerType}");
            return 1.0; // Neutre par défaut
        }

        var advantages = typeAdvantages[attackerType];
        if (!advantages.ContainsKey(targetType))
        {
            Console.WriteLine($"[INFO] Type cible inconnu ou neutre pour {attackerType} → {targetType}");
        }

        return advantages.ContainsKey(targetType) ? advantages[targetType] : 1.0;
    }

    // Nouvelle méthode pour afficher une description de l'efficacité
    public static string GetEffectivenessDescription(double multiplier)
    {
        return multiplier switch
        {
            > 1.0 => "Super efficace !",
            < 1.0 and > 0.0 => "Pas très efficace...",
            0.0 => "Cela n'a aucun effet...",
            _ => "C'est neutre."
        };
    }

}
