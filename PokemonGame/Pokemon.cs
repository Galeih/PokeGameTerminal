namespace PokemonGame;

public class Pokemon(string name, int level, int health, int attack, string type1, string type2 = null)
{
    private static readonly Random RandomInstance = new();

    public string Name { get; set; } = name;
    public int Level { get; set; } = level;
    public int Health { get; set; } = health;
    public int Attack { get; set; } = attack;
    public int Defense { get; set; } = level * 2; // Défense initiale proportionnelle au niveau
    public int Speed { get; set; } = level * 3;   // Vitesse initiale proportionnelle au niveau
    public int Experience { get; set; } = 0;      // XP actuel
    public string Type1 { get; set; } = type1;
    public string? Type2 { get; set; } = type2;

    public void AttackPokemon(Pokemon target)
    {
        // Combine les deux types du target
        double multiplierT1 = GetSingleTypeMultiplier(this.Type1, target.Type1);
        double multiplierT2 = target.Type2 != null
            ? GetSingleTypeMultiplier(this.Type1, target.Type2)
            : 1.0;

        double finalMultiplier = multiplierT1 * multiplierT2;

        int damage = (int)((this.Attack - target.Defense / 2.0) * finalMultiplier);
        damage = Math.Max(1, damage); // Les dégâts doivent être au minimum de 1
        target.Health -= damage;

        Console.WriteLine($"{Name} attaque {target.Name} et inflige {damage} dégâts !");
        Console.WriteLine($"{target.Name} a maintenant {Math.Max(0, target.Health)} PV.");
    }

    public bool IsFainted() => Health <= 0;

    public void GainExperience(int exp)
    {
        this.Experience += exp; // Ajout XP

        while (this.Experience >= Level * 5)
        {
            this.Experience -= Level * 5;
            Level++;
            Health += 7;
            Attack += 4;
            Defense += 2;
            Speed += 3;
            Console.WriteLine($"{Name} monte au niveau {Level} !");
        }
    }

    public void Heal()
    {
        int maxHealth = Level * 12;
        Health = maxHealth;
        Console.WriteLine($"{Name} a récupéré tous ses PV (PV max : {maxHealth}) !");
    }

    /// <summary>
    /// Calcule un multiplicateur en fonction des types attaquant et défendeur.
    /// </summary>
    private static double GetSingleTypeMultiplier(string attackerType, string targetType)
    {
        Dictionary<string, Dictionary<string, double>> typeAdvantages = new()
        {
            { "Normal", new()
                {
                    { "Roche", 0.5 }, { "Acier", 0.5 }, { "Spectre", 0.0 }
                }},
            { "Feu", new()
                {
                    { "Eau", 0.5 }, { "Feu", 0.5 }, { "Plante", 2.0 }, { "Insecte", 2.0 },
                    { "Acier", 2.0 }, { "Roche", 0.5 }, { "Dragon", 0.5 }
                }},
            { "Eau", new()
                {
                    { "Feu", 2.0 }, { "Eau", 0.5 }, { "Roche", 2.0 }, { "Sol", 2.0 }, { "Dragon", 0.5 }
                }},
            { "Plante", new()
                {
                    { "Feu", 0.5 }, { "Eau", 2.0 }, { "Plante", 0.5 }, { "Vol", 0.5 }, { "Insecte", 0.5 },
                    { "Roche", 2.0 }, { "Sol", 2.0 }, { "Dragon", 0.5 }, { "Acier", 0.5 }
                }},
            { "Électrique", new()
                {
                    { "Eau", 2.0 }, { "Plante", 0.5 }, { "Sol", 0.0 }, { "Vol", 2.0 }, { "Dragon", 0.5 }
                }},
            { "Glace", new()
                {
                    { "Feu", 0.5 }, { "Eau", 0.5 }, { "Plante", 2.0 }, { "Glace", 0.5 },
                    { "Sol", 2.0 }, { "Vol", 2.0 }, { "Dragon", 2.0 }, { "Acier", 0.5 }
                }},
            { "Combat", new()
                {
                    { "Normal", 2.0 }, { "Glace", 2.0 }, { "Roche", 2.0 }, { "Spectre", 0.0 },
                    { "Poison", 0.5 }, { "Vol", 0.5 }, { "Psy", 0.5 }, { "Insecte", 0.5 }, { "Fée", 0.5 }
                }},
            { "Poison", new()
                {
                    { "Plante", 2.0 }, { "Poison", 0.5 }, { "Sol", 0.5 }, { "Roche", 0.5 },
                    { "Spectre", 0.5 }, { "Acier", 0.0 }
                }},
            { "Sol", new()
                {
                    { "Feu", 2.0 }, { "Électrique", 2.0 }, { "Poison", 2.0 }, { "Roche", 2.0 },
                    { "Vol", 0.0 }, { "Plante", 0.5 }, { "Insecte", 0.5 }
                }},
            { "Vol", new()
                {
                    { "Plante", 2.0 }, { "Combat", 2.0 }, { "Insecte", 2.0 },
                    { "Roche", 0.5 }, { "Électrique", 0.5 }, { "Acier", 0.5 }
                }},
            { "Psy", new()
                {
                    { "Combat", 2.0 }, { "Poison", 2.0 }, { "Acier", 0.5 }, { "Psy", 0.5 },
                    { "Ténèbres", 0.0 }
                }},
            { "Insecte", new()
                {
                    { "Plante", 2.0 }, { "Psy", 2.0 }, { "Ténèbres", 2.0 },
                    { "Feu", 0.5 }, { "Combat", 0.5 }, { "Vol", 0.5 }, { "Spectre", 0.5 },
                    { "Acier", 0.5 }, { "Fée", 0.5 }
                }},
            { "Roche", new()
                {
                    { "Feu", 2.0 }, { "Glace", 2.0 }, { "Vol", 2.0 }, { "Insecte", 2.0 },
                    { "Combat", 0.5 }, { "Sol", 0.5 }, { "Acier", 0.5 }
                }},
            { "Spectre", new()
                {
                    { "Normal", 0.0 }, { "Psy", 2.0 }, { "Spectre", 2.0 }, { "Ténèbres", 0.5 }
                }},
            { "Dragon", new()
                {
                    { "Dragon", 2.0 }, { "Acier", 0.5 }, { "Fée", 0.0 }
                }},
            { "Ténèbres", new()
                {
                    { "Psy", 2.0 }, { "Spectre", 2.0 }, { "Combat", 0.5 }, { "Ténèbres", 0.5 }, { "Fée", 0.5 }
                }},
            { "Acier", new()
                {
                    { "Glace", 2.0 }, { "Roche", 2.0 }, { "Fée", 2.0 },
                    { "Feu", 0.5 }, { "Eau", 0.5 }, { "Électrique", 0.5 }, { "Acier", 0.5 }
                }},
            { "Fée", new()
                {
                    { "Combat", 2.0 }, { "Dragon", 2.0 }, { "Ténèbres", 2.0 },
                    { "Feu", 0.5 }, { "Poison", 0.5 }, { "Acier", 0.5 }
                }}
        };

        // Si le type n'est pas trouvé, retourne un multiplicateur neutre
        if (!typeAdvantages.ContainsKey(attackerType))
            return 1.0;

        Dictionary<string, double> advantages = typeAdvantages[attackerType];
        if (advantages.ContainsKey(targetType))
            return advantages[targetType];

        return 1.0;
    }

    public static Pokemon GenerateWildPokemon(int zoneLevel)
    {
        List<Pokemon> wildPokemons = new()
        {
            new("Rattata", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Normal"),
            new("Pidgey", 4 + zoneLevel, 30 + (zoneLevel * 5), 6 + (zoneLevel * 2), "Vol", "Normal"),
            new("Zubat", 2 + zoneLevel, 20 + (zoneLevel * 5), 4 + (zoneLevel * 2), "Poison", "Vol"),
            new("Ekans", 5 + zoneLevel, 35 + (zoneLevel * 5), 7 + (zoneLevel * 2), "Poison"),
            new("Geodude", 3 + zoneLevel, 40 + (zoneLevel * 5), 6 + (zoneLevel * 2), "Roche", "Sol")
        };

        return wildPokemons[RandomInstance.Next(wildPokemons.Count)];
    }
}
