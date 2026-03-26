using System.Text.Json;

namespace PokemonGame.Services;

public sealed class PokeApiClient
{
    private static readonly HttpClient Http = new()
    {
        BaseAddress = new Uri("https://pokeapi.co/api/v2/")
    };

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly string _cacheDirectory;

    public PokeApiClient()
    {
        _cacheDirectory = Path.Combine(AppContext.BaseDirectory, "cache");
        Directory.CreateDirectory(_cacheDirectory);
    }

    public async Task<PokeApiListResponse> GetPokemonListAsync(int limit = 151, int offset = 0)
    {
        string cacheKey = $"pokemon-list-{limit}-{offset}.json";
        return await GetWithCacheAsync<PokeApiListResponse>($"pokemon?limit={limit}&offset={offset}", cacheKey);
    }

    public async Task<PokeApiPokemon> GetPokemonAsync(string nameOrId)
    {
        string key = SanitizeForFileName(nameOrId);
        return await GetWithCacheAsync<PokeApiPokemon>($"pokemon/{nameOrId}", $"pokemon-{key}.json");
    }

    public async Task<PokeApiMove> GetMoveAsync(string nameOrId)
    {
        string key = SanitizeForFileName(nameOrId);
        return await GetWithCacheAsync<PokeApiMove>($"move/{nameOrId}", $"move-{key}.json");
    }

    public async Task<List<PokeApiEncounter>> GetPokemonEncountersAsync(string nameOrId)
    {
        string key = SanitizeForFileName(nameOrId);
        return await GetWithCacheAsync<List<PokeApiEncounter>>($"pokemon/{nameOrId}/encounters", $"encounters-{key}.json");
    }

    private async Task<T> GetWithCacheAsync<T>(string endpoint, string cacheFileName)
    {
        string cachePath = Path.Combine(_cacheDirectory, cacheFileName);

        try
        {
            using HttpResponseMessage response = await Http.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            await File.WriteAllTextAsync(cachePath, json);

            T? data = JsonSerializer.Deserialize<T>(json, _jsonOptions);
            if (data == null)
            {
                throw new InvalidOperationException($"Réponse vide pour {endpoint}");
            }

            return data;
        }
        catch
        {
            if (!File.Exists(cachePath))
            {
                throw;
            }

            string cachedJson = await File.ReadAllTextAsync(cachePath);
            T? cachedData = JsonSerializer.Deserialize<T>(cachedJson, _jsonOptions);

            if (cachedData == null)
            {
                throw;
            }

            return cachedData;
        }
    }

    private static string SanitizeForFileName(string value)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            value = value.Replace(c, '-');
        }

        return value.ToLowerInvariant();
    }
}
