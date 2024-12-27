namespace PokemonGame;

public static class WildPokemonData
{
    private static readonly Dictionary<string, List<Pokemon>> WildPokemonByZone = new()
    {
        { "Forêt", new()
            {
                new("Caterpie", 2, 20, 4, "Insecte"),
                new("Weedle", 2, 20, 4, "Insecte", "Poison"),
                new("Oddish", 3, 25, 5, "Plante", "Poison"),
                new("Paras", 3, 25, 5, "Insecte", "Plante"),
                new("Pidgey", 3, 25, 5, "Vol", "Normal"),
                new("Rattata", 3, 25, 5, "Normal"),
                new("Venonat", 3, 25, 5, "Insecte", "Poison"),
            }
        },
        { "Montagne", new()
            {
                new("Geodude", 3, 40, 6, "Roche", "Sol"),
                new("Machop", 4, 30, 6, "Combat"),
                new("Mankey", 4, 30, 6, "Combat"),
                new("Onix", 5, 50, 8, "Roche", "Sol"),
                new("Sandshrew", 3, 25, 5, "Sol"),
                new("Cubone", 4, 30, 6, "Sol"),
            }
        },
        { "Lac", new()
            {
                new("Magikarp", 2, 20, 4, "Eau"),
                new("Poliwag", 3, 25, 5, "Eau"),
                new("Goldeen", 4, 30, 6, "Eau"),
                new("Psyduck", 4, 30, 6, "Eau"),
                new("Horsea", 4, 30, 6, "Eau"),
                new("Staryu", 5, 35, 7, "Eau"),
                new("Shellder", 3, 25, 5, "Eau"),
            }
        },
        { "Grotte", new()
            {
                new("Zubat", 3, 25, 5, "Poison", "Vol"),
                new("Geodude", 3, 40, 6, "Roche", "Sol"),
                new("Onix", 5, 50, 8, "Roche", "Sol"),
                new("Clefairy", 4, 30, 6, "Fée"),
                new("Gastly", 3, 25, 5, "Spectre", "Poison"),
            }
        },
        { "Plaine", new()
            {
                new("Meowth", 3, 25, 5, "Normal"),
                new("Doduo", 3, 25, 5, "Normal", "Vol"),
                new("Farfetch'd", 3, 25, 5, "Normal", "Vol"),
                new("Eevee", 4, 30, 6, "Normal"),
                new("Tauros", 5, 35, 7, "Normal"),
                new("Snorlax", 5, 50, 8, "Normal"),
            }
        },
        { "Zone industrielle", new()
            {
                new("Koffing", 3, 25, 5, "Poison"),
                new("Grimer", 3, 25, 5, "Poison"),
                new("Magnemite", 4, 30, 6, "Électrique", "Acier"),
                new("Voltorb", 5, 35, 7, "Électrique"),
            }
        },
        { "Zone rare", new()
            {
                new("Dratini", 5, 35, 7, "Dragon"),
                new("Aerodactyl", 4, 30, 6, "Roche", "Vol"),
                new("Kabuto", 5, 35, 7, "Roche", "Eau"),
                new("Omanyte", 3, 25, 5, "Roche", "Eau"),
                new("Ditto", 3, 25, 5, "Normal"),
            }
        }
    };

    /// <summary>
    /// Génère un Pokémon sauvage en fonction de la zone et du niveau.
    /// </summary>
    /// <param name="zoneLevel">Le niveau de la zone.</param>
    /// <param name="zoneName">Le nom de la zone.</param>
    /// <returns>Un Pokémon généré.</returns>
    public static Pokemon GenerateWildPokemon(int zoneLevel, string zoneName)
    {
        if (!WildPokemonByZone.ContainsKey(zoneName))
            throw new ArgumentException($"Zone inconnue : {zoneName}");

        List<Pokemon> wildPokemons = WildPokemonByZone[zoneName];

        Pokemon basePokemon = wildPokemons[RandomInstance.Next(wildPokemons.Count)];

        return new Pokemon(
            basePokemon.Name,
            basePokemon.Level + zoneLevel,
            basePokemon.Health + zoneLevel * 5,
            basePokemon.Attack + zoneLevel * 2,
            basePokemon.Type1,
            basePokemon.Type2
        );
    }

    private static readonly Random RandomInstance = new();
}
