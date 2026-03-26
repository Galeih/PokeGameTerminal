using PokemonGame.Services;

namespace PokemonGame;

public static class WildPokemonData
{
    private static readonly PokeApiClient PokeApiClient = new();
    private static readonly Random RandomInstance = new();

    private static readonly Dictionary<string, List<Pokemon>> WildPokemonByZone = new()
    {
        { "Forêt", new() { new("Caterpie", 2, 20, 4, "Insecte"), new("Weedle", 2, 20, 4, "Insecte", "Poison"), new("Oddish", 3, 25, 5, "Plante", "Poison"), new("Paras", 3, 25, 5, "Insecte", "Plante"), new("Pidgey", 3, 25, 5, "Vol", "Normal"), new("Rattata", 3, 25, 5, "Normal"), new("Venonat", 3, 25, 5, "Insecte", "Poison") } },
        { "Montagne", new() { new("Geodude", 3, 40, 6, "Roche", "Sol"), new("Machop", 4, 30, 6, "Combat"), new("Mankey", 4, 30, 6, "Combat"), new("Onix", 5, 50, 8, "Roche", "Sol"), new("Sandshrew", 3, 25, 5, "Sol"), new("Cubone", 4, 30, 6, "Sol") } },
        { "Lac", new() { new("Magikarp", 2, 20, 4, "Eau"), new("Poliwag", 3, 25, 5, "Eau"), new("Goldeen", 4, 30, 6, "Eau"), new("Psyduck", 4, 30, 6, "Eau"), new("Horsea", 4, 30, 6, "Eau"), new("Staryu", 5, 35, 7, "Eau"), new("Shellder", 3, 25, 5, "Eau") } },
        { "Grotte", new() { new("Zubat", 3, 25, 5, "Poison", "Vol"), new("Geodude", 3, 40, 6, "Roche", "Sol"), new("Onix", 5, 50, 8, "Roche", "Sol"), new("Clefairy", 4, 30, 6, "Fée"), new("Gastly", 3, 25, 5, "Spectre", "Poison") } },
        { "Plaine", new() { new("Meowth", 3, 25, 5, "Normal"), new("Doduo", 3, 25, 5, "Normal", "Vol"), new("Farfetch'd", 3, 25, 5, "Normal", "Vol"), new("Eevee", 4, 30, 6, "Normal"), new("Tauros", 5, 35, 7, "Normal"), new("Snorlax", 5, 50, 8, "Normal") } },
        { "Zone industrielle", new() { new("Koffing", 3, 25, 5, "Poison"), new("Grimer", 3, 25, 5, "Poison"), new("Magnemite", 4, 30, 6, "Électrique", "Acier"), new("Voltorb", 5, 35, 7, "Électrique") } },
        { "Zone rare", new() { new("Dratini", 5, 35, 7, "Dragon"), new("Aerodactyl", 4, 30, 6, "Roche", "Vol"), new("Kabuto", 5, 35, 7, "Roche", "Eau"), new("Omanyte", 3, 25, 5, "Roche", "Eau"), new("Ditto", 3, 25, 5, "Normal") } }
    };

    public static Pokemon GenerateWildPokemon(int zoneLevel, string zoneName)
    {
        try
        {
            int randomId = RandomInstance.Next(1, 152);
            int level = Math.Max(2, RandomInstance.Next(2, 6) + zoneLevel);
            return BuildPokemonFromApi(randomId.ToString(), level, fallbackLocation: zoneName);
        }
        catch
        {
            return GenerateWildPokemonFallback(zoneLevel, zoneName);
        }
    }

    public static Pokemon CreatePokemonFromApiOrFallback(string apiName, int level, Pokemon fallback)
    {
        try
        {
            return BuildPokemonFromApi(apiName, level);
        }
        catch
        {
            fallback.Level = level;
            fallback.Health = level * 12;
            if (fallback.Moves.Count == 0)
            {
                fallback.LearnMove(new AttackLogic("Charge", "Normal", "Physique", 40, 100));
            }

            return fallback;
        }
    }

    private static Pokemon BuildPokemonFromApi(string pokemonIdOrName, int level, string? fallbackLocation = null)
    {
        PokeApiPokemon apiPokemon = PokeApiClient.GetPokemonAsync(pokemonIdOrName).GetAwaiter().GetResult();

        string type1 = apiPokemon.Types.Count > 0 ? PokeApiMapper.ToGameType(apiPokemon.Types[0].Type.Name) : "Normal";
        string? type2 = apiPokemon.Types.Count > 1 ? PokeApiMapper.ToGameType(apiPokemon.Types[1].Type.Name) : null;

        int hpBase = GetStat(apiPokemon, "hp", 45);
        int atkBase = GetStat(apiPokemon, "attack", 49);
        int defBase = GetStat(apiPokemon, "defense", 49);
        int spaBase = GetStat(apiPokemon, "special-attack", 50);
        int spdBase = GetStat(apiPokemon, "special-defense", 50);
        int speBase = GetStat(apiPokemon, "speed", 45);

        var pokemon = new Pokemon(
            PokeApiMapper.ToDisplayName(apiPokemon.Name),
            level,
            CalculateHp(hpBase, level),
            CalculateOtherStat(atkBase, level),
            type1,
            type2,
            captureRate: 160);

        pokemon.Defense = CalculateOtherStat(defBase, level);
        pokemon.SpecialAttack = CalculateOtherStat(spaBase, level);
        pokemon.SpecialDefense = CalculateOtherStat(spdBase, level);
        pokemon.Speed = CalculateOtherStat(speBase, level);

        List<PokeApiEncounter> encounters = PokeApiClient.GetPokemonEncountersAsync(pokemonIdOrName).GetAwaiter().GetResult();
        pokemon.EncounterLocation = encounters.Count > 0
            ? PokeApiMapper.ToDisplayName(encounters[0].LocationArea.Name)
            : fallbackLocation ?? "Inconnu";

        AddApiMoves(apiPokemon, pokemon, level);

        if (pokemon.Moves.Count == 0)
        {
            pokemon.LearnMove(new AttackLogic("Charge", "Normal", "Physique", 40, 100));
        }

        return pokemon;
    }

    private static int CalculateHp(int baseStat, int level)
    {
        return Math.Max(10, ((2 * baseStat) * level / 100) + level + 10);
    }

    private static int CalculateOtherStat(int baseStat, int level)
    {
        return Math.Max(5, ((2 * baseStat) * level / 100) + 5);
    }

    private static int GetStat(PokeApiPokemon pokemon, string statName, int fallback)
    {
        PokeApiPokemonStatEntry? stat = pokemon.Stats.FirstOrDefault(s => s.Stat.Name == statName);
        return stat?.BaseStat ?? fallback;
    }

    private static void AddApiMoves(PokeApiPokemon apiPokemon, Pokemon pokemon, int level)
    {
        List<string> levelMoves = apiPokemon.Moves
            .Where(m => m.VersionGroupDetails.Any(v => v.MoveLearnMethod.Name == "level-up" && v.LevelLearnedAt <= level))
            .OrderByDescending(m => m.VersionGroupDetails.Where(v => v.MoveLearnMethod.Name == "level-up").Max(v => v.LevelLearnedAt))
            .Select(m => m.Move.Name)
            .Distinct()
            .Take(4)
            .ToList();

        IEnumerable<string> candidates = levelMoves.Count > 0
            ? levelMoves
            : apiPokemon.Moves.Select(m => m.Move.Name).Distinct().OrderBy(_ => RandomInstance.Next()).Take(20);

        foreach (string moveName in candidates)
        {
            if (pokemon.Moves.Count >= 4)
            {
                break;
            }

            try
            {
                PokeApiMove move = PokeApiClient.GetMoveAsync(moveName).GetAwaiter().GetResult();
                int power = move.Power ?? 0;
                int accuracy = move.Accuracy ?? 100;
                string category = PokeApiMapper.ToGameCategory(move.DamageClass.Name);
                string type = PokeApiMapper.ToGameType(move.Type.Name);

                if (category != "Soutien" && power <= 0)
                {
                    continue;
                }

                pokemon.LearnMove(new AttackLogic(PokeApiMapper.ToDisplayName(move.Name), type, category, power, accuracy));
            }
            catch
            {
                // Continue sur la prochaine attaque.
            }
        }
    }

    private static Pokemon GenerateWildPokemonFallback(int zoneLevel, string zoneName)
    {
        if (!WildPokemonByZone.TryGetValue(zoneName, out List<Pokemon>? wildPokemons))
        {
            throw new ArgumentException($"Zone inconnue : {zoneName}");
        }

        Pokemon basePokemon = wildPokemons[RandomInstance.Next(wildPokemons.Count)];
        Pokemon generated = new(
            basePokemon.Name,
            basePokemon.Level + zoneLevel,
            basePokemon.Health + zoneLevel * 5,
            basePokemon.Attack + zoneLevel * 2,
            basePokemon.Type1,
            basePokemon.Type2);

        generated.EncounterLocation = zoneName;
        generated.LearnMove(new AttackLogic("Charge", "Normal", "Physique", 40, 100));
        return generated;
    }
}
