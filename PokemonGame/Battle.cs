namespace PokemonGame;

public class Battle
{
    private Player Player;
    private Pokemon WildPokemon;

    public Battle(Player player, Pokemon wildPokemon)
    {
        Player = player;
        WildPokemon = wildPokemon;
    }

    public void StartBattle()
    {
        Pokemon playerPokemon = Player.Pokemons.FirstOrDefault(p => !p.IsFainted());
        if (playerPokemon == null)
        {
            Console.WriteLine("Tous vos Pokémon sont K.O. Vous ne pouvez pas combattre !");
            return;
        }

        Console.WriteLine($"Un {WildPokemon.Name} sauvage apparaît !");
        while (!playerPokemon.IsFainted() && !WildPokemon.IsFainted())
        {
            AfficherBarreDeVie(playerPokemon, WildPokemon);

            // Tour du joueur
            Console.WriteLine("Choisissez une attaque :");
            for (int i = 0; i < playerPokemon.Moves.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {playerPokemon.Moves[i].Name} ({playerPokemon.Moves[i].Category})");
            }

            int choice = int.Parse(Console.ReadLine() ?? "1") - 1;
            if (choice >= 0 && choice < playerPokemon.Moves.Count)
            {
                playerPokemon.UseMove(playerPokemon.Moves[choice].Name, WildPokemon);
            }

            if (WildPokemon.IsFainted())
            {
                Console.WriteLine($"{WildPokemon.Name} est K.O. !");
                break;
            }

            // Tour du Pokémon sauvage
            Random random = new();
            int randomMoveIndex = random.Next(WildPokemon.Moves.Count);
            WildPokemon.UseMove(WildPokemon.Moves[randomMoveIndex].Name, playerPokemon);

            if (playerPokemon.IsFainted())
            {
                Console.WriteLine($"{playerPokemon.Name} est K.O. !");
                if (Player.Pokemons.Any(p => !p.IsFainted()))
                {
                    Console.WriteLine("Choisissez un autre Pokémon !");
                    playerPokemon = Player.Pokemons.First(p => !p.IsFainted());
                }
                else
                {
                    Console.WriteLine("Tous vos Pokémon sont K.O. !");
                    break;
                }
            }
        }
    }

    private void AfficherBarreDeVie(Pokemon p1, Pokemon p2)
    {
        Console.WriteLine("\n╔════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║ {p1.Name,-15} | PV: {p1.Health}/{p1.Level * 12} | {GetBarreDeVie(p1.Health, p1.Level * 12)} ║");
        Console.WriteLine("╠════════════════════════════════════════════════════════════════════╣");
        Console.WriteLine($"║ {p2.Name,-15} | PV: {p2.Health}/{p2.Level * 12} | {GetBarreDeVie(p2.Health, p2.Level * 12)} ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
    }

    private string GetBarreDeVie(int current, int max)
    {
        const int totalBarres = 20;
        int barresRemplies = (int)((current / (double)max) * totalBarres);
        int barresVides = totalBarres - barresRemplies;

        string color = current > max * 0.5 ? "green" : current > max * 0.2 ? "yellow" : "red";

        string filledBar = new string('█', barresRemplies);
        string emptyBar = new string('░', barresVides);

        return $"[bold {color}]{filledBar}[/]{emptyBar}";
    }
}
