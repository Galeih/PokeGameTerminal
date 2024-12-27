namespace PokemonGame;

public class Player(string name)
{
    public string Name { get; set; } = name;
    public List<Pokemon> Pokemons { get; set; } = new();
    public int Money { get; set; } = 100;
    public int PokeBalls { get; set; } = 5;
    public int ZoneLevel { get; set; } = 1;
    public Dictionary<string, int> Inventory { get; set; } = new()
    {
        { "Potion", 3 },
        { "Super Potion", 1 }
    };

    public void AddPokemon(Pokemon pokemon)
    {
        if (Pokemons.Count < 6)
        {
            Pokemons.Add(pokemon);
            Console.WriteLine($"{pokemon.Name} rejoint votre équipe !");
        }
        else
        {
            Console.WriteLine("Votre équipe est pleine ! Relâchez un Pokémon pour en ajouter un nouveau.");
        }
    }

    public void BuyItem(string item, int cost)
    {
        if (Money < cost)
        {
            Console.WriteLine("Vous n'avez pas assez d'argent !");
            return;
        }

        Money -= cost;
        if (Inventory.ContainsKey(item))
        {
            Inventory[item]++;
        }
        else
        {
            Inventory[item] = 1;
        }

        Console.WriteLine($"Vous avez acheté {item}. Il vous reste {Money} pièces.");
    }

    public Pokemon ChoosePokemon()
    {
        Console.WriteLine("Choisissez un Pokémon :");
        for (int i = 0; i < Pokemons.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Pokemons[i].Name} (PV : {Pokemons[i].Health})");
        }

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > Pokemons.Count)
        {
            Console.WriteLine("Entrée invalide. Veuillez choisir un numéro valide.");
        }

        return Pokemons[choice - 1];
    }

    public bool TryCatchPokemon(Pokemon wildPokemon)
    {
        if (PokeBalls <= 0)
        {
            Console.WriteLine("Vous n'avez plus de Pokéballs !");
            return false;
        }

        PokeBalls--;
        Console.WriteLine($"Vous utilisez une Pokéball. Il vous en reste {PokeBalls}.");

        Random random = new();
        int chance = random.Next(40, 101);
        int captureRate = Math.Max(10, (100 * wildPokemon.Health) / (wildPokemon.Level * 15));

        if (chance <= captureRate)
        {
            Console.WriteLine($"Félicitations ! Vous avez capturé {wildPokemon.Name} !");
            AddPokemon(wildPokemon);
            return true;
        }
        else
        {
            Console.WriteLine($"Oh non ! {wildPokemon.Name} s'est échappé !");
            return false;
        }
    }
}
