namespace PokemonGame;

public class Pokemon(string name, int level, int health, int attack, string type1, string? type2 = null)
{
    public string Name { get; set; } = name;

    public int Level { get; set; } = level;

    public int Health { get; set; } = health;

    public int Attack { get; set; } = attack;

    public int Defense { get; set; } = level * 2;

    public int SpecialAttack { get; set; } = level * 3; // Exemples

    public int SpecialDefense { get; set; } = level * 2; // Exemples

    public int Speed { get; set; } = level * 3; // Exemples

    public string Type1 { get; set; } = type1;

    public string? Type2 { get; set; } = type2;

    public int Experience { get; set; } = 0;

    public List<AttackLogic> Moves { get; set; } = new();

    private static readonly Random RandomInstance = new();

    public void AddMove(AttackLogic attack)
    {
        if (Moves.Count < 4)
            Moves.Add(attack);
        else
            Console.WriteLine("Impossible d'apprendre plus de 4 attaques !");
    }

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
            new("Abra", 6 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Psy"),
            new("Aerodactyl", 4 + zoneLevel, 30 + (zoneLevel * 5), 6 + (zoneLevel * 2), "Roche", "Vol"),
            new("Bellsprout", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Plante", "Poison"),
            new("Bulbasaur", 5 + zoneLevel, 35 + (zoneLevel * 5), 7 + (zoneLevel * 2), "Plante", "Poison"),
            new("Caterpie", 2 + zoneLevel, 20 + (zoneLevel * 5), 4 + (zoneLevel * 2), "Insecte"),
            new("Charmander", 5 + zoneLevel, 35 + (zoneLevel * 5), 7 + (zoneLevel * 2), "Feu"),
            new("Clefairy", 4 + zoneLevel, 30 + (zoneLevel * 5), 6 + (zoneLevel * 2), "Fée"),
            new("Cubone", 4 + zoneLevel, 30 + (zoneLevel * 5), 6 + (zoneLevel * 2), "Sol"),
            new("Diglett", 4 + zoneLevel, 30 + (zoneLevel * 5), 6 + (zoneLevel * 2), "Sol"),
            new("Ditto", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Normal"),
            new("Doduo", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Normal", "Vol"),
            new("Dratini", 5 + zoneLevel, 35 + (zoneLevel * 5), 7 + (zoneLevel * 2), "Dragon"),
            new("Drowzee", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Psy"),
            new("Eevee", 4 + zoneLevel, 30 + (zoneLevel * 5), 6 + (zoneLevel * 2), "Normal"),
            new("Ekans", 5 + zoneLevel, 35 + (zoneLevel * 5), 7 + (zoneLevel * 2), "Poison"),
            new("Exeggcute", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Plante", "Psy"),
            new("Farfetch'd", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Normal", "Vol"),
            new("Gastly", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Spectre", "Poison"),
            new("Geodude", 3 + zoneLevel, 40 + (zoneLevel * 5), 6 + (zoneLevel * 2), "Roche", "Sol"),
            new("Goldeen", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Eau"),
            new("Grimer", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Poison"),
            new("Growlithe", 4 + zoneLevel, 30 + (zoneLevel * 5), 6 + (zoneLevel * 2), "Feu"),
            new("Horsea", 4 + zoneLevel, 30 + (zoneLevel * 5), 6 + (zoneLevel * 2), "Eau"),
            new("Jigglypuff", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Normal", "Fée"),
            new("Kabuto", 5 + zoneLevel, 35 + (zoneLevel * 5), 7 + (zoneLevel * 2), "Roche", "Eau"),
            new("Koffing", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Poison"),
            new("Krabby", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Eau"),
            new("Lickitung", 4 + zoneLevel, 30 + (zoneLevel * 5), 6 + (zoneLevel * 2), "Normal"),
            new("Machop", 4 + zoneLevel, 30 + (zoneLevel * 5), 6 + (zoneLevel * 2), "Combat"),
            new("Magikarp", 2 + zoneLevel, 20 + (zoneLevel * 5), 4 + (zoneLevel * 2), "Eau"),
            new("Magnemite", 4 + zoneLevel, 30 + (zoneLevel * 5), 6 + (zoneLevel * 2), "Électrique", "Acier"),
            new("Mankey", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Combat"),
            new("Meowth", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Normal"),
            new("Nidoran♀", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Poison"),
            new("Nidoran♂", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Poison"),
            new("Oddish", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Plante", "Poison"),
            new("Omanyte", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Roche", "Eau"),
            new("Paras", 2 + zoneLevel, 20 + (zoneLevel * 5), 4 + (zoneLevel * 2), "Insecte", "Plante"),
            new("Pidgey", 4 + zoneLevel, 30 + (zoneLevel * 5), 6 + (zoneLevel * 2), "Vol", "Normal"),
            new("Poliwag", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Eau"),
            new("Ponyta", 4 + zoneLevel, 30 + (zoneLevel * 5), 6 + (zoneLevel * 2), "Feu"),
            new("Psyduck", 4 + zoneLevel, 30 + (zoneLevel * 5), 6 + (zoneLevel * 2), "Eau"),
            new("Rattata", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Normal"),
            new("Rhyhorn", 5 + zoneLevel, 35 + (zoneLevel * 5), 7 + (zoneLevel * 2), "Sol", "Roche"),
            new("Sandshrew", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Sol"),
            new("Seel", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Eau"),
            new("Shellder", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Eau"),
            new("Snorlax", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Normal"),
            new("Spearow", 2 + zoneLevel, 20 + (zoneLevel * 5), 4 + (zoneLevel * 2), "Normal", "Vol"),
            new("Squirtle", 5 + zoneLevel, 35 + (zoneLevel * 5), 7 + (zoneLevel * 2), "Eau"),
            new("Staryu", 5 + zoneLevel, 35 + (zoneLevel * 5), 7 + (zoneLevel * 2), "Eau"),
            new("Tentacool", 5 + zoneLevel, 35 + (zoneLevel * 5), 7 + (zoneLevel * 2), "Eau", "Poison"),
            new("Venonat", 3 + zoneLevel, 25 + (zoneLevel * 5), 5 + (zoneLevel * 2), "Insecte", "Poison"),
            new("Voltorb", 5 + zoneLevel, 35 + (zoneLevel * 5), 7 + (zoneLevel * 2), "Électrique"),
            new("Vulpix", 4 + zoneLevel, 30 + (zoneLevel * 5), 6 + (zoneLevel * 2), "Feu"),
            new("Weedle", 2 + zoneLevel, 20 + (zoneLevel * 5), 4 + (zoneLevel * 2), "Insecte", "Poison"),
            new("Zubat", 2 + zoneLevel, 20 + (zoneLevel * 5), 4 + (zoneLevel * 2), "Poison", "Vol"),
        };

        return wildPokemons[RandomInstance.Next(wildPokemons.Count)];
    }
}
