using Spectre.Console;

namespace PokemonGame;

public class Battle
{
    private readonly Player _player;
    private readonly Pokemon _wildPokemon;

    public bool WasWildPokemonDefeated { get; private set; }

    public Battle(Player player, Pokemon wildPokemon)
    {
        _player = player;
        _wildPokemon = wildPokemon;
        _wildPokemon.Health = _wildPokemon.Level * 12;
    }

    public void StartBattle()
    {
        Pokemon? playerPokemon = _player.Pokemons.FirstOrDefault(p => !p.IsFainted());
        if (playerPokemon == null)
        {
            AnsiConsole.MarkupLine("[bold red]Tous vos Pokémon sont K.O. Vous ne pouvez pas combattre ![/]");
            return;
        }

        if (_wildPokemon.Moves.Count == 0)
        {
            _wildPokemon.LearnMove(new AttackLogic("Charge", "Normal", "Physique", 40, 100));
        }

        AnsiConsole.MarkupLine($"[bold yellow]Un {_wildPokemon.Name} sauvage apparaît ![/]");

        while (!playerPokemon.IsFainted() && !_wildPokemon.IsFainted())
        {
            AfficherBarreDeVie(playerPokemon, _wildPokemon);

            string action = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold green]Que voulez-vous faire ?[/]")
                    .AddChoices("Attaquer", "Sac", "Pokémon", "Capturer", "Fuite"));

            switch (action)
            {
                case "Attaquer":
                    ChoisirAttaque(playerPokemon, _wildPokemon);
                    break;
                case "Sac":
                    if (UtiliserObjet(playerPokemon))
                    {
                        break;
                    }
                    continue;
                case "Pokémon":
                    Pokemon? nouveauPokemon = ChangerPokemon(playerPokemon);
                    if (nouveauPokemon == null || nouveauPokemon == playerPokemon)
                    {
                        AnsiConsole.MarkupLine("[bold yellow]Vous avez annulé le changement de Pokémon.[/]");
                        continue;
                    }

                    playerPokemon = nouveauPokemon;
                    AnsiConsole.MarkupLine($"[bold green]Allez, {playerPokemon.Name} ![/]");
                    break;
                case "Capturer":
                    if (CapturerPokemon(_wildPokemon))
                    {
                        return;
                    }

                    AnsiConsole.MarkupLine("[bold red]Le Pokémon sauvage s'est échappé de la Pokéball ![/]");
                    break;
                case "Fuite":
                    if (TenterFuite(playerPokemon, _wildPokemon))
                    {
                        AnsiConsole.MarkupLine("[bold yellow]Vous avez réussi à fuir le combat ![/]");
                        return;
                    }

                    AnsiConsole.MarkupLine("[bold red]Vous n'avez pas pu fuir ![/]");
                    break;
            }

            if (_wildPokemon.IsFainted())
            {
                WasWildPokemonDefeated = true;
                AnsiConsole.MarkupLine($"[bold green]{_wildPokemon.Name} est K.O. ![/]");
                break;
            }

            int randomMoveIndex = Random.Shared.Next(_wildPokemon.Moves.Count);
            _wildPokemon.UseMove(_wildPokemon.Moves[randomMoveIndex].Name, playerPokemon);

            if (!playerPokemon.IsFainted())
            {
                continue;
            }

            AnsiConsole.MarkupLine($"[bold red]{playerPokemon.Name} est K.O. ![/]");
            if (_player.Pokemons.Any(p => !p.IsFainted()))
            {
                AnsiConsole.MarkupLine("[bold yellow]Choisissez un autre Pokémon ![/]");
                Pokemon? remplacement = ChangerPokemon(playerPokemon);
                if (remplacement != null)
                {
                    playerPokemon = remplacement;
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[bold red]Tous vos Pokémon sont K.O. ![/]");
                break;
            }
        }
    }

    private void ChoisirAttaque(Pokemon playerPokemon, Pokemon target)
    {
        var choixAttaque = new SelectionPrompt<string>()
            .Title("[bold cyan]Choisissez une attaque :[/]")
            .AddChoices(playerPokemon.Moves.Select((move, index) => $"{index + 1}. {move.Name} ({move.Category})").ToList());

        string choix = AnsiConsole.Prompt(choixAttaque);
        int choiceIndex = int.Parse(choix.Split('.')[0]) - 1;

        if (choiceIndex >= 0 && choiceIndex < playerPokemon.Moves.Count)
        {
            playerPokemon.UseMove(playerPokemon.Moves[choiceIndex].Name, target);
        }
    }

    private bool UtiliserObjet(Pokemon playerPokemon)
    {
        var choixObjet = new SelectionPrompt<string>()
            .Title("[bold cyan]Choisissez un objet :[/]")
            .AddChoices("Potion", "Super Potion", "Pokéball", "Retour");

        string choix = AnsiConsole.Prompt(choixObjet);

        switch (choix)
        {
            case "Potion":
                if (!_player.TryUseItem("Potion"))
                {
                    AnsiConsole.MarkupLine("[bold red]Vous n'avez plus de Potion.[/]");
                    return false;
                }

                playerPokemon.Health = Math.Min(playerPokemon.Level * 12, playerPokemon.Health + 20);
                AnsiConsole.MarkupLine($"[bold green]Vous utilisez une Potion. Stock restant : {_player.GetItemCount("Potion")}[/]");
                return true;
            case "Super Potion":
                if (!_player.TryUseItem("Super Potion"))
                {
                    AnsiConsole.MarkupLine("[bold red]Vous n'avez plus de Super Potion.[/]");
                    return false;
                }

                playerPokemon.Health = Math.Min(playerPokemon.Level * 12, playerPokemon.Health + 50);
                AnsiConsole.MarkupLine($"[bold green]Vous utilisez une Super Potion. Stock restant : {_player.GetItemCount("Super Potion")}[/]");
                return true;
            case "Pokéball":
                AnsiConsole.MarkupLine("[bold yellow]Utilisez l'option 'Capturer' pour lancer une Pokéball.[/]");
                return false;
            case "Retour":
                AnsiConsole.MarkupLine("[bold yellow]Vous retournez au menu principal.[/]");
                return false;
            default:
                return false;
        }
    }

    private Pokemon? ChangerPokemon(Pokemon currentPokemon)
    {
        List<string> availablePokemons = _player.Pokemons
            .Where(p => !p.IsFainted())
            .Select(p => $"{p.Name} (PV : {Math.Max(0, p.Health)}/{p.Level * 12})")
            .ToList();

        if (availablePokemons.Count == 0)
        {
            return null;
        }

        string choix = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold cyan]Choisissez un Pokémon :[/]")
                .AddChoices(availablePokemons));

        string nomPokemon = choix.Split('(')[0].Trim();
        Pokemon? nouveauPokemon = _player.Pokemons.FirstOrDefault(p => p.Name == nomPokemon);

        if (nouveauPokemon == currentPokemon)
        {
            return null;
        }

        return nouveauPokemon;
    }

    private bool CapturerPokemon(Pokemon target)
    {
        if (!_player.TryUseItem("Pokéball"))
        {
            AnsiConsole.MarkupLine("[bold red]Vous n'avez plus de Pokéballs ![/]");
            return false;
        }

        AnsiConsole.MarkupLine($"[bold yellow]Vous utilisez une Pokéball. Il vous en reste {_player.GetItemCount("Pokéball")}.[/]");

        int maxHealth = Math.Max(1, target.Level * 12);
        int currentHealth = Math.Clamp(target.Health, 1, maxHealth);

        double tauxCaptureBase = target.CaptureRate / 255.0;
        double tauxVie = 1.0 - (currentHealth / (double)maxHealth);
        double bonusStatut = target.Status == "Paralysé" || target.Status == "Gelé"
            ? 0.2
            : target.Status == "Endormi"
                ? 0.3
                : 0.0;

        double tauxFinal = Math.Clamp(tauxCaptureBase * (0.35 + tauxVie + bonusStatut), 0.05, 0.95);
        if (Random.Shared.NextDouble() < tauxFinal)
        {
            AnsiConsole.MarkupLine($"[bold green]Félicitations ! Vous avez capturé {target.Name} ![/]");
            _player.AddPokemon(target);
            return true;
        }

        AnsiConsole.MarkupLine($"[bold red]{target.Name} s'est échappé ![/]");
        return false;
    }

    private static bool TenterFuite(Pokemon playerPokemon, Pokemon wildPokemon)
    {
        int chance = playerPokemon.Speed >= wildPokemon.Speed ? 65 : 40;
        return Random.Shared.Next(0, 100) < chance;
    }

    private void AfficherBarreDeVie(Pokemon p1, Pokemon p2)
    {
        Table table = new Table()
            .AddColumn("[bold]Pokémon[/]")
            .AddColumn("[bold]PV[/]")
            .AddColumn("[bold]Barre de Vie[/]");

        table.AddRow(
            p1.Name,
            $"{Math.Max(0, p1.Health)}/{p1.Level * 12}",
            GetBarreDeVie(Math.Max(0, p1.Health), p1.Level * 12)
        );

        table.AddRow(
            p2.Name,
            $"{Math.Max(0, p2.Health)}/{p2.Level * 12}",
            GetBarreDeVie(Math.Max(0, p2.Health), p2.Level * 12)
        );

        AnsiConsole.Write(table);
    }

    private static string GetBarreDeVie(int current, int max)
    {
        const int totalBarres = 20;
        int barresRemplies = (int)((current / (double)max) * totalBarres);
        int barresVides = totalBarres - barresRemplies;

        string color = current > max * 0.5 ? "green" : current > max * 0.2 ? "yellow" : "red";

        string filledBar = new('█', barresRemplies);
        string emptyBar = new('░', barresVides);

        return $"[bold {color}]{filledBar}[/]{emptyBar}";
    }
}
