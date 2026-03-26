using Spectre.Console;

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

    public string? Status { get; set; } // Ex: Paralysé, Endormi, etc.

    public string? EncounterLocation { get; set; }

    public int CaptureRate { get; set; } // Nouveau : Taux de capture de base du Pokémon

    // Liste des attaques
    public List<AttackLogic> Moves { get; set; } = new();

    // Constructeur
    public Pokemon(string name, int level, int health, int attack, string type1, string? type2 = null, int captureRate = 255)
    {
        Name = name;
        Level = level;
        Health = health;
        Attack = attack;
        Defense = level * 2;
        SpecialAttack = level * 3;
        SpecialDefense = level * 2;
        Speed = level * 3;
        Experience = 0;
        Type1 = type1;
        Type2 = type2;
        Status = null;
        CaptureRate = captureRate; // Valeur par défaut : 255 (le plus facile à capturer)
    }

    private static readonly Dictionary<string, HashSet<string>> StrongAgainst = new()
    {
        ["Normal"] = [],
        ["Feu"] = ["Plante", "Glace", "Insecte", "Acier"],
        ["Eau"] = ["Feu", "Sol", "Roche"],
        ["Plante"] = ["Eau", "Sol", "Roche"],
        ["Électrique"] = ["Eau", "Vol"],
        ["Glace"] = ["Plante", "Sol", "Vol", "Dragon"],
        ["Combat"] = ["Normal", "Glace", "Roche", "Ténèbres", "Acier"],
        ["Poison"] = ["Plante", "Fée"],
        ["Sol"] = ["Feu", "Électrique", "Poison", "Roche", "Acier"],
        ["Vol"] = ["Plante", "Combat", "Insecte"],
        ["Psy"] = ["Combat", "Poison"],
        ["Insecte"] = ["Plante", "Psy", "Ténèbres"],
        ["Roche"] = ["Feu", "Glace", "Vol", "Insecte"],
        ["Spectre"] = ["Psy", "Spectre"],
        ["Dragon"] = ["Dragon"],
        ["Ténèbres"] = ["Psy", "Spectre"],
        ["Acier"] = ["Glace", "Roche", "Fée"],
        ["Fée"] = ["Combat", "Dragon", "Ténèbres"]
    };

    private static readonly Dictionary<string, HashSet<string>> WeakAgainst = new()
    {
        ["Normal"] = ["Roche", "Acier"],
        ["Feu"] = ["Feu", "Eau", "Roche", "Dragon"],
        ["Eau"] = ["Eau", "Plante", "Dragon"],
        ["Plante"] = ["Feu", "Plante", "Poison", "Vol", "Insecte", "Dragon", "Acier"],
        ["Électrique"] = ["Plante", "Électrique", "Dragon"],
        ["Glace"] = ["Feu", "Eau", "Glace", "Acier"],
        ["Combat"] = ["Poison", "Vol", "Psy", "Insecte", "Fée"],
        ["Poison"] = ["Poison", "Sol", "Roche", "Spectre"],
        ["Sol"] = ["Plante", "Insecte"],
        ["Vol"] = ["Électrique", "Roche", "Acier"],
        ["Psy"] = ["Psy", "Acier"],
        ["Insecte"] = ["Feu", "Combat", "Poison", "Vol", "Spectre", "Acier", "Fée"],
        ["Roche"] = ["Combat", "Sol", "Acier"],
        ["Spectre"] = ["Ténèbres"],
        ["Dragon"] = ["Acier"],
        ["Ténèbres"] = ["Combat", "Ténèbres", "Fée"],
        ["Acier"] = ["Feu", "Eau", "Électrique", "Acier"],
        ["Fée"] = ["Feu", "Poison", "Acier"]
    };

    private static readonly Dictionary<string, HashSet<string>> NoEffectAgainst = new()
    {
        ["Normal"] = ["Spectre"],
        ["Électrique"] = ["Sol"],
        ["Combat"] = ["Spectre"],
        ["Poison"] = ["Acier"],
        ["Sol"] = ["Vol"],
        ["Psy"] = ["Ténèbres"],
        ["Spectre"] = ["Normal"],
        ["Dragon"] = ["Fée"]
    };

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
        var strongAgainst = new Dictionary<string, HashSet<string>>
        {
            ["Normal"] = [],
            ["Feu"] = ["Plante", "Glace", "Insecte", "Acier"],
            ["Eau"] = ["Feu", "Sol", "Roche"],
            ["Plante"] = ["Eau", "Sol", "Roche"],
            ["Électrique"] = ["Eau", "Vol"],
            ["Glace"] = ["Plante", "Sol", "Vol", "Dragon"],
            ["Combat"] = ["Normal", "Glace", "Roche", "Ténèbres", "Acier"],
            ["Poison"] = ["Plante", "Fée"],
            ["Sol"] = ["Feu", "Électrique", "Poison", "Roche", "Acier"],
            ["Vol"] = ["Plante", "Combat", "Insecte"],
            ["Psy"] = ["Combat", "Poison"],
            ["Insecte"] = ["Plante", "Psy", "Ténèbres"],
            ["Roche"] = ["Feu", "Glace", "Vol", "Insecte"],
            ["Spectre"] = ["Psy", "Spectre"],
            ["Dragon"] = ["Dragon"],
            ["Ténèbres"] = ["Psy", "Spectre"],
            ["Acier"] = ["Glace", "Roche", "Fée"],
            ["Fée"] = ["Combat", "Dragon", "Ténèbres"]
        };

        var weakAgainst = new Dictionary<string, HashSet<string>>
        {
            ["Normal"] = ["Roche", "Acier"],
            ["Feu"] = ["Feu", "Eau", "Roche", "Dragon"],
            ["Eau"] = ["Eau", "Plante", "Dragon"],
            ["Plante"] = ["Feu", "Plante", "Poison", "Vol", "Insecte", "Dragon", "Acier"],
            ["Électrique"] = ["Plante", "Électrique", "Dragon"],
            ["Glace"] = ["Feu", "Eau", "Glace", "Acier"],
            ["Combat"] = ["Poison", "Vol", "Psy", "Insecte", "Fée"],
            ["Poison"] = ["Poison", "Sol", "Roche", "Spectre"],
            ["Sol"] = ["Plante", "Insecte"],
            ["Vol"] = ["Électrique", "Roche", "Acier"],
            ["Psy"] = ["Psy", "Acier"],
            ["Insecte"] = ["Feu", "Combat", "Poison", "Vol", "Spectre", "Acier", "Fée"],
            ["Roche"] = ["Combat", "Sol", "Acier"],
            ["Spectre"] = ["Ténèbres"],
            ["Dragon"] = ["Acier"],
            ["Ténèbres"] = ["Combat", "Ténèbres", "Fée"],
            ["Acier"] = ["Feu", "Eau", "Électrique", "Acier"],
            ["Fée"] = ["Feu", "Poison", "Acier"]
        };

        var noEffectAgainst = new Dictionary<string, HashSet<string>>
        {
            ["Normal"] = ["Spectre"],
            ["Électrique"] = ["Sol"],
            ["Combat"] = ["Spectre"],
            ["Poison"] = ["Acier"],
            ["Sol"] = ["Vol"],
            ["Psy"] = ["Ténèbres"],
            ["Spectre"] = ["Normal"],
            ["Dragon"] = ["Fée"]
        };

        if (noEffectAgainst.TryGetValue(attackerType, out HashSet<string>? immuneTypes) && immuneTypes.Contains(targetType))
        {
            return 0.0;
        }

        if (strongAgainst.TryGetValue(attackerType, out HashSet<string>? superEffectiveTypes) && superEffectiveTypes.Contains(targetType))
        {
            return 2.0;
        }

        if (weakAgainst.TryGetValue(attackerType, out HashSet<string>? notVeryEffectiveTypes) && notVeryEffectiveTypes.Contains(targetType))
        {
            return 0.5;
        }

        return 1.0;
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
