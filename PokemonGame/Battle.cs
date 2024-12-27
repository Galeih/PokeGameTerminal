using Spectre.Console;

namespace PokemonGame;

public class Battle
{
    private Player Player;
    private Pokemon WildPokemon;

    public Battle(Player player, Pokemon wildPokemon)
    {
        Player = player;
        WildPokemon = wildPokemon;
        WildPokemon.Health = WildPokemon.Level * 12; // Maximiser les PV du Pokémon adverse
    }

    public void StartBattle()
    {
        Pokemon playerPokemon = Player.Pokemons.FirstOrDefault(p => !p.IsFainted());
        if (playerPokemon == null)
        {
            AnsiConsole.MarkupLine("[bold red]Tous vos Pokémon sont K.O. Vous ne pouvez pas combattre ![/]");
            return;
        }

        AnsiConsole.MarkupLine($"[bold yellow]Un {WildPokemon.Name} sauvage apparaît ![/]");

        while (!playerPokemon.IsFainted() && !WildPokemon.IsFainted())
        {
            AfficherBarreDeVie(playerPokemon, WildPokemon);

            // Menu principal du combat
            string action = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold green]Que voulez-vous faire ?[/]")
                    .AddChoices("Attaquer", "Sac", "Pokémon", "Capturer", "Fuite"));

            switch (action)
            {
                case "Attaquer":
                    ChoisirAttaque(playerPokemon, WildPokemon);
                    break;
                case "Sac":
                    if (UtiliserObjet(playerPokemon))
                    {
                        break; // Passe un tour uniquement si un objet a été utilisé
                    }
                    continue; // Retour au menu principal sans passer de tour
                case "Pokémon":
                    var nouveauPokemon = ChangerPokemon(playerPokemon);
                    if (nouveauPokemon == null || nouveauPokemon == playerPokemon)
                    {
                        AnsiConsole.MarkupLine("[bold yellow]Vous avez annulé le changement de Pokémon.[/]");
                        continue;
                    }
                    playerPokemon = nouveauPokemon;
                    AnsiConsole.MarkupLine($"[bold green]Allez, {playerPokemon.Name} ![/]");
                    break;
                case "Capturer":
                    if (CapturerPokemon(WildPokemon))
                    {
                        AnsiConsole.MarkupLine($"[bold green]Félicitations ! Vous avez capturé {WildPokemon.Name} ![/]");
                        Player.AddPokemon(WildPokemon);
                        return; // Le combat se termine après la capture.
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[bold red]Le Pokémon sauvage s'est échappé de la Pokéball ![/]");
                    }
                    break;
                case "Fuite":
                    if (TenterFuite(playerPokemon, WildPokemon))
                    {
                        AnsiConsole.MarkupLine("[bold yellow]Vous avez réussi à fuir le combat ![/]");
                        return;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[bold red]Vous n'avez pas pu fuir ![/]");
                    }
                    break;
            }

            if (WildPokemon.IsFainted())
            {
                AnsiConsole.MarkupLine($"[bold green]{WildPokemon.Name} est K.O. ![/]");
                break;
            }

            // Tour du Pokémon sauvage
            Random random = new();
            int randomMoveIndex = random.Next(WildPokemon.Moves.Count);
            WildPokemon.UseMove(WildPokemon.Moves[randomMoveIndex].Name, playerPokemon);

            if (playerPokemon.IsFainted())
            {
                AnsiConsole.MarkupLine($"[bold red]{playerPokemon.Name} est K.O. ![/]");
                if (Player.Pokemons.Any(p => !p.IsFainted()))
                {
                    AnsiConsole.MarkupLine("[bold yellow]Choisissez un autre Pokémon ![/]");
                    playerPokemon = ChangerPokemon(playerPokemon);
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold red]Tous vos Pokémon sont K.O. ![/]");
                    break;
                }
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
                playerPokemon.Health = Math.Min(playerPokemon.Level * 12, playerPokemon.Health + 20);
                AnsiConsole.MarkupLine("[bold green]Vous utilisez une Potion. PV restaurés ![/]");
                return true; // Compte comme un tour
            case "Super Potion":
                playerPokemon.Health = Math.Min(playerPokemon.Level * 12, playerPokemon.Health + 50);
                AnsiConsole.MarkupLine("[bold green]Vous utilisez une Super Potion. PV restaurés ![/]");
                return true; // Compte comme un tour
            case "Pokéball":
                AnsiConsole.MarkupLine("[bold yellow]Vous ne pouvez pas capturer un Pokémon avec un objet ici. Utilisez l'option 'Capturer'.[/]");
                return false; // Ne compte pas comme un tour
            case "Retour":
                AnsiConsole.MarkupLine("[bold yellow]Vous retournez au menu principal.[/]");
                return false; // Ne compte pas comme un tour
        }

        return false; // Par défaut, ne compte pas comme un tour
    }

    private Pokemon? ChangerPokemon(Pokemon currentPokemon)
    {
        var choixPokemon = new SelectionPrompt<string>()
            .Title("[bold cyan]Choisissez un Pokémon :[/]")
            .AddChoices(Player.Pokemons
                .Where(p => !p.IsFainted())
                .Select(p => $"{p.Name} (PV : {p.Health}/{p.Level * 12})")
                .ToList());

        string choix = AnsiConsole.Prompt(choixPokemon);
        string nomPokemon = choix.Split('(')[0].Trim();

        var nouveauPokemon = Player.Pokemons.FirstOrDefault(p => p.Name == nomPokemon);

        if (nouveauPokemon == currentPokemon)
        {
            return null; // Annule si le Pokémon sélectionné est déjà en combat.
        }

        return nouveauPokemon;
    }

    private bool CapturerPokemon(Pokemon target)
    {
        if (Player.PokeBalls <= 0)
        {
            AnsiConsole.MarkupLine("[bold red]Vous n'avez plus de Pokéballs ![/]");
            return false;
        }

        Player.PokeBalls--;
        AnsiConsole.MarkupLine($"[bold yellow]Vous utilisez une Pokéball. Il vous en reste {Player.PokeBalls}.[/]");

        // Calcul du taux de capture
        Random random = new();
        double tauxCaptureBase = target.CaptureRate / 255.0; // Taux de capture normalisé (0 à 1)
        double tauxVie = 1.0 - (target.Health / (double)(target.Level * 12)); // En fonction des PV restants
        double bonusStatut = target.Status == "Paralysé" || target.Status == "Gelé" ? 0.2 : target.Status == "Endormi" ? 0.3 : 0.0;

        double tauxFinal = Math.Min(0.95, tauxCaptureBase * (tauxVie + bonusStatut)); // Limité à 95%
        if (random.NextDouble() < tauxFinal)
        {
            AnsiConsole.MarkupLine($"[bold green]Félicitations ! Vous avez capturé {target.Name} ![/]");
            Player.AddPokemon(target);
            return true;
        }
        else
        {
            AnsiConsole.MarkupLine($"[bold red]{target.Name} s'est échappé ![/]");
            return false;
        }
    }


    private bool TenterFuite(Pokemon playerPokemon, Pokemon wildPokemon)
    {
        Random random = new();
        return random.Next(0, 100) < 50; // 50% de chance de fuite
    }

    private void AfficherBarreDeVie(Pokemon p1, Pokemon p2)
    {
        Table table = new Table()
            .AddColumn("[bold]Pokémon[/]")
            .AddColumn("[bold]PV[/]")
            .AddColumn("[bold]Barre de Vie[/]");

        table.AddRow(
            p1.Name,
            $"{p1.Health}/{p1.Level * 12}",
            GetBarreDeVie(p1.Health, p1.Level * 12)
        );

        table.AddRow(
            p2.Name,
            $"{p2.Health}/{p2.Level * 12}",
            GetBarreDeVie(p2.Health, p2.Level * 12)
        );

        AnsiConsole.Write(table);
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
