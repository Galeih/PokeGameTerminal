using PokemonGame.Services;

namespace PokemonGame;

public static class WildPokemonData
{
    private static readonly PokeApiClient PokeApiClient = new();
    private static readonly Random RandomInstance = new();

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

    public static Pokemon GenerateWildPokemon(int zoneLevel, string zoneName)
    {
        try
        {
            return GenerateWildPokemonFromApi(zoneLevel);
        }
        catch
        {
            return GenerateWildPokemonFallback(zoneLevel, zoneName);
        }
    }

    private static Pokemon GenerateWildPokemonFromApi(int zoneLevel)
    {
        int generationLimit = 151;
        int randomId = RandomInstance.Next(1, generationLimit + 1);
        var apiPokemon = PokeApiClient.GetPokemonAsync(randomId.ToString()).GetAwaiter().GetResult();

        string type1 = apiPokemon.Types.Count > 0
            ? PokeApiMapper.ToGameType(apiPokemon.Types[0].Type.Name)
            : "Normal";

        string? type2 = apiPokemon.Types.Count > 1
            ? PokeApiMapper.ToGameType(apiPokemon.Types[1].Type.Name)
            : null;

        int level = Math.Max(2, RandomInstance.Next(2, 6) + zoneLevel);
        int hpBase = GetStat(apiPokemon, "hp", fallback: 45);
        int attackBase = GetStat(apiPokemon, "attack", fallback: 49);
        int defenseBase = GetStat(apiPokemon, "defense", fallback: 49);
        int specialAttackBase = GetStat(apiPokemon, "special-attack", fallback: 50);
        int specialDefenseBase = GetStat(apiPokemon, "special-defense", fallback: 50);
        int speedBase = GetStat(apiPokemon, "speed", fallback: 45);

        var pokemon = new Pokemon(
            PokeApiMapper.ToDisplayName(apiPokemon.Name),
            level,
            Math.Max(level * 8, hpBase + level * 2),
            Math.Max(4, attackBase / 2 + level),
            type1,
            type2,
            captureRate: 160
        );

        pokemon.Defense = Math.Max(4, defenseBase / 2 + level);
        pokemon.SpecialAttack = Math.Max(4, specialAttackBase / 2 + level);
        pokemon.SpecialDefense = Math.Max(4, specialDefenseBase / 2 + level);
        pokemon.Speed = Math.Max(4, speedBase / 2 + level);

        var encounters = PokeApiClient.GetPokemonEncountersAsync(randomId.ToString()).GetAwaiter().GetResult();
        pokemon.EncounterLocation = encounters.Count > 0
            ? PokeApiMapper.ToDisplayName(encounters[0].LocationArea.Name)
            : "Inconnu";

        AddApiMoves(apiPokemon, pokemon);

        if (pokemon.Moves.Count == 0)
        {
            pokemon.LearnMove(new AttackLogic("Charge", "Normal", "Physique", 40, 100));
        }

        return pokemon;
    }

    private static int GetStat(PokeApiPokemon pokemon, string statName, int fallback)
    {
        PokeApiPokemonStatEntry? stat = pokemon.Stats.FirstOrDefault(s => s.Stat.Name == statName);
        return stat?.BaseStat ?? fallback;
    }

    private static void AddApiMoves(PokeApiPokemon apiPokemon, Pokemon pokemon)
    {
        var candidates = apiPokemon.Moves
            .Select(m => m.Move.Name)
            .Distinct()
            .OrderBy(_ => RandomInstance.Next())
            .Take(20)
            .ToList();

        foreach (string moveName in candidates)
        {
            if (pokemon.Moves.Count >= 4)
            {
                break;
            }

            try
            {
                var move = PokeApiClient.GetMoveAsync(moveName).GetAwaiter().GetResult();
                int power = move.Power ?? 0;
                int accuracy = move.Accuracy ?? 100;
                string category = PokeApiMapper.ToGameCategory(move.DamageClass.Name);
                string type = PokeApiMapper.ToGameType(move.Type.Name);
                string displayName = PokeApiMapper.ToDisplayName(move.Name);

                if (category != "Soutien" && power <= 0)
                {
                    continue;
                }

                pokemon.LearnMove(new AttackLogic(displayName, type, category, power, accuracy));
            }
            catch
            {
                // Ignore les attaques non récupérables et continue.
            }
        }
    }

    private static Pokemon GenerateWildPokemonFallback(int zoneLevel, string zoneName)
    {
        if (!WildPokemonByZone.ContainsKey(zoneName))
        {
            throw new ArgumentException($"Zone inconnue : {zoneName}");
        }

        List<Pokemon> wildPokemons = WildPokemonByZone[zoneName];
        Pokemon basePokemon = wildPokemons[RandomInstance.Next(wildPokemons.Count)];

        Pokemon generated = new(
            basePokemon.Name,
            basePokemon.Level + zoneLevel,
            basePokemon.Health + zoneLevel * 5,
            basePokemon.Attack + zoneLevel * 2,
            basePokemon.Type1,
            basePokemon.Type2
        );
        generated.EncounterLocation = zoneName;

        generated.LearnMove(new AttackLogic("Charge", "Normal", "Physique", 40, 100));
        generated.LearnMove(new AttackLogic("Morsure", "Ténèbres", "Physique", 60, 100));

        return generated;
    }
}
