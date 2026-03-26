using System.Text.Json.Serialization;

namespace PokemonGame.Services;

public sealed class PokeApiNamedResource
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

public sealed class PokeApiListResponse
{
    [JsonPropertyName("results")]
    public List<PokeApiNamedResource> Results { get; set; } = new();
}

public sealed class PokeApiPokemonTypeEntry
{
    [JsonPropertyName("type")]
    public PokeApiNamedResource Type { get; set; } = new();
}

public sealed class PokeApiPokemonMoveEntry
{
    [JsonPropertyName("move")]
    public PokeApiNamedResource Move { get; set; } = new();
}

public sealed class PokeApiPokemon
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("base_experience")]
    public int BaseExperience { get; set; }

    [JsonPropertyName("types")]
    public List<PokeApiPokemonTypeEntry> Types { get; set; } = new();

    [JsonPropertyName("moves")]
    public List<PokeApiPokemonMoveEntry> Moves { get; set; } = new();
}

public sealed class PokeApiMove
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("power")]
    public int? Power { get; set; }

    [JsonPropertyName("accuracy")]
    public int? Accuracy { get; set; }

    [JsonPropertyName("type")]
    public PokeApiNamedResource Type { get; set; } = new();

    [JsonPropertyName("damage_class")]
    public PokeApiNamedResource DamageClass { get; set; } = new();
}
