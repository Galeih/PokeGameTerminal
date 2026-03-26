namespace PokemonGame;

public class Player(string name)
{
    public string Name { get; set; } = name;
    public List<Pokemon> Pokemons { get; set; } = new();
    public int Money { get; set; } = 100;
    public int ZoneLevel { get; set; } = 1;
    public Dictionary<string, int> Inventory { get; set; } = new()
    {
        { "Potion", 3 },
        { "Super Potion", 1 },
        { "Pokéball", 5 }
    };

    public int PokeBalls
    {
        get => GetItemCount("Pokéball");
        set => Inventory["Pokéball"] = Math.Max(0, value);
    }

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

    public void EarnMoney(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        Money += amount;
        Console.WriteLine($"Vous gagnez {amount} pièces. Total : {Money}.");
    }

    public int GetItemCount(string item)
    {
        return Inventory.TryGetValue(item, out int count) ? count : 0;
    }

    public bool TryUseItem(string item)
    {
        if (GetItemCount(item) <= 0)
        {
            Console.WriteLine($"Vous n'avez plus de {item}.");
            return false;
        }

        Inventory[item]--;
        return true;
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
        if (!TryUseItem("Pokéball"))
        {
            Console.WriteLine("Vous n'avez plus de Pokéballs !");
            return false;
        }

        Console.WriteLine($"Vous utilisez une Pokéball. Il vous en reste {GetItemCount("Pokéball")}.");

        Random random = new();
        int chance = random.Next(1, 101);
        int maxHealth = Math.Max(1, wildPokemon.Level * 12);
        int currentHealth = Math.Clamp(wildPokemon.Health, 1, maxHealth);
        int captureRate = Math.Clamp(90 - (currentHealth * 70 / maxHealth), 15, 95);

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
