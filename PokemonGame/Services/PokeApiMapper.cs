namespace PokemonGame.Services;

public static class PokeApiMapper
{
    private static readonly Dictionary<string, string> TypeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["normal"] = "Normal",
        ["fire"] = "Feu",
        ["water"] = "Eau",
        ["grass"] = "Plante",
        ["electric"] = "Électrique",
        ["ice"] = "Glace",
        ["fighting"] = "Combat",
        ["poison"] = "Poison",
        ["ground"] = "Sol",
        ["flying"] = "Vol",
        ["psychic"] = "Psy",
        ["bug"] = "Insecte",
        ["rock"] = "Roche",
        ["ghost"] = "Spectre",
        ["dragon"] = "Dragon",
        ["dark"] = "Ténèbres",
        ["steel"] = "Acier",
        ["fairy"] = "Fée"
    };

    private static readonly Dictionary<string, string> CategoryMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["physical"] = "Physique",
        ["special"] = "Spéciale",
        ["status"] = "Soutien"
    };

    public static string ToGameType(string apiType)
    {
        return TypeMap.TryGetValue(apiType, out string? mapped) ? mapped : apiType;
    }

    public static string ToGameCategory(string apiCategory)
    {
        return CategoryMap.TryGetValue(apiCategory, out string? mapped) ? mapped : "Soutien";
    }

    public static string ToDisplayName(string apiName)
    {
        if (string.IsNullOrWhiteSpace(apiName))
        {
            return apiName;
        }

        string[] parts = apiName.Split('-', StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i] = char.ToUpper(parts[i][0]) + parts[i][1..];
        }

        return string.Join(' ', parts);
    }
}
