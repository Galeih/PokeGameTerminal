﻿using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PokemonGame;

class Program
{
    static void Main(string[] args)
    {
        // Affiche la bannière d'accueil style "Pokémon Rouge"
        AfficherBanniere();

        // Demande du nom de l'entraîneur
        string playerName = AnsiConsole.Ask<string>("[yellow]Entrez votre nom dresseur :[/] ");
        SimulerChargement("Création de votre profil");

        // Création du joueur et variables
        Player player = new(playerName);
        int battleCount = 0;

        // Sélection du Pokémon de départ
        AfficherCadre("CHOISISSEZ VOTRE POKÉMON DE DÉPART", ConsoleColor.Magenta);
        string starterChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold cyan]Choisissez un Pokémon de départ :[/]")
                .AddChoices(
                    "Pikachu ⚡ (Type : Électrique)",
                    "Salamèche 🔥 (Type : Feu)",
                    "Carapuce 🌊 (Type : Eau)")
        );

        Pokemon starter = starterChoice switch
        {
            "Pikachu ⚡ (Type : Électrique)" => new Pokemon("Pikachu", 5, 35, 10, "Électrique"),
            "Salamèche 🔥 (Type : Feu)" => new Pokemon("Salamèche", 5, 30, 12, "Feu"),
            "Carapuce 🌊 (Type : Eau)" => new Pokemon("Carapuce", 5, 40, 8, "Eau"),
            _ => throw new InvalidOperationException()
        };
        starter.Heal(); // Pour s'assurer qu'il a Level * 12 PV soit full HP

        AnsiConsole.MarkupLine($"[bold green]🎉 {starter.Name} a rejoint votre équipe ![/]");
        player.AddPokemon(starter);

        // Boucle principale : menu du jeu
        while (true)
        {
            // Menu principal
            var action = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold green]MENU PRINCIPAL[/]")
                    .AddChoices(
                        "Combattre un Pokémon sauvage",
                        "Voir votre équipe",
                        "Soigner votre équipe",
                        "Statistiques",
                        "Boutique",
                        "Quitter"));

            // Si tous les Pokémon sont K.O. (sauf si on veut soigner)
            if (player.Pokemons.All(p => p.IsFainted()) && action != "Soigner votre équipe")
            {
                AfficherCadre("Tous vos Pokémon sont K.O. ! Vous devez les soigner avant de continuer.", ConsoleColor.Red);
                continue;
            }

            switch (action)
            {
                case "Combattre un Pokémon sauvage":
                    CombatSauvage(ref battleCount, player);
                    break;
                case "Voir votre équipe":
                    VoirEquipeOuResume(player);
                    break;
                case "Soigner votre équipe":
                    SoignerEquipe(player);
                    break;
                case "Statistiques":
                    AfficherStatistiques(player, battleCount);
                    break;
                case "Boutique":
                    Boutique(player);
                    break;
                case "Quitter":
                    AfficherCadre("Merci d'avoir joué ! À bientôt !", ConsoleColor.Red);
                    return;
            }
        }
    }

    /// <summary>
    /// Affiche une bannière stylée "Pokémon Rouge".
    /// </summary>
    static void AfficherBanniere()
    {
        var banner = new Panel(@"
[bold yellow]BIENVENUE À KANTO !
LE MONDE DES POKÉMON ![/]")
            .Header("[bold red]Pokémon Rouge[/]", Justify.Center)
            .Border(BoxBorder.Double)
            .Expand()
            .Padding(2, 1);

        AnsiConsole.Write(banner);
    }

    /// <summary>
    /// Affiche un cadre stylisé avec un message, possibilité de changer la couleur.
    /// </summary>
    static void AfficherCadre(string message, ConsoleColor color = ConsoleColor.Yellow)
    {
        AnsiConsole.Write(
            new Panel($"[bold {color.ToString().ToLower()}]{message}[/]")
                .Border(BoxBorder.Rounded)
                .Padding(1, 1)
                .Expand()
        );
    }

    /// <summary>
    /// Simule un chargement stylisé.
    /// </summary>
    static void SimulerChargement(string message)
    {
        AnsiConsole.Status()
            .Start(message, ctx =>
            {
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(50);
                }
            });
    }

    /// <summary>
    /// Calcule l'XP gagnée en fonction du niveau du Pokémon vaincu avec une variation de ±5 %.
    /// </summary>
    /// <param name="level">Niveau du Pokémon vaincu.</param>
    /// <returns>XP gagnée.</returns>
    public static int CalculerExperienceGagnee(int level)
    {
        // Base XP = Niveau du Pokémon vaincu × 10
        double baseXp = level * 10;

        // Variation aléatoire entre -5 % et +5 %
        Random random = new();
        double variationPourcentage = random.NextDouble() * 10 - 5; // Entre -5 et +5
        double xpAvecVariation = baseXp * (1 + variationPourcentage / 100);

        // Retourne l'XP arrondie à l'entier le plus proche
        return (int)Math.Round(xpAvecVariation);
    }


    /// <summary>
    /// Gère un combat contre un Pokémon sauvage.
    /// </summary>
    static void CombatSauvage(ref int battleCount, Player player)
    {
        var wildPokemon = Pokemon.GenerateWildPokemon(player.ZoneLevel);
        AfficherCadre($"Un [bold yellow]{wildPokemon.Name}[/] sauvage apparaît !", ConsoleColor.Blue);

        SimulerChargement("Le combat commence");

        // Utilise la classe Battle pour lancer le combat
        Battle battle = new(player, new Player("Sauvage") { Pokemons = { wildPokemon } });
        battle.StartWithHealthBar();

        // Si le Pokémon adverse est K.O. => possibilité de capture
        if (wildPokemon.IsFainted())
        {
            bool capture = AnsiConsole.Confirm("[bold yellow]Le Pokémon ennemi est K.O. Voulez-vous le capturer ?[/]");
            if (capture)
            {
                player.TryCatchPokemon(wildPokemon);
            }
        }

        battleCount++;
    }

    /// <summary>
    /// Permet de voir l'équipe du joueur ou afficher le résumé détaillé d'un Pokémon.
    /// </summary>
    static void VoirEquipeOuResume(Player player)
    {
        // Affichage de l'équipe
        AfficherEquipe(player);

        // Proposer de voir un résumé spécifique
        if (AnsiConsole.Confirm("Voulez-vous consulter le résumé d'un Pokémon ?"))
        {
            // Sélection du Pokémon à voir
            var pkmnChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold blue]Choisissez un Pokémon pour voir son résumé :[/]")
                    .AddChoices(player.Pokemons.Select(pk => pk.Name))
            );

            var chosenPokemon = player.Pokemons.FirstOrDefault(pk => pk.Name == pkmnChoice);
            if (chosenPokemon != null)
            {
                AfficherResumePokemon(chosenPokemon);
            }
        }
    }

    /// <summary>
    /// Affiche l'équipe du joueur avec un tableau stylisé.
    /// </summary>
    static void AfficherEquipe(Player player)
    {
        var table = new Table().Centered();
        table.AddColumn("[bold]Nom[/]");
        table.AddColumn("[bold]Type[/]");
        table.AddColumn("[bold]PV[/]");

        foreach (Pokemon p in player.Pokemons)
        {
            string typeText = p.Type2 == null
                ? p.Type1
                : $"{p.Type1} / {p.Type2}";
            table.AddRow(p.Name, typeText, $"{p.Health}/{p.Level * 12}");
        }

        AnsiConsole.Write(new Panel(table)
            .Header("VOTRE ÉQUIPE", Justify.Center)
            .Border(BoxBorder.Rounded)
            .Expand());
    }

    /// <summary>
    /// Affiche un résumé détaillé d'un Pokémon (stats, XP manquante, etc.).
    /// </summary>
    static void AfficherResumePokemon(Pokemon p)
    {
        // Calcul de l'XP requise pour le niveau suivant (ex: level * 5 => total, p.Experience => actuel)
        int expNeeded = p.Level * 5;
        int missingExp = expNeeded - p.Experience;
        if (missingExp < 0) missingExp = 0;

        var detailTable = new Table();
        detailTable.AddColumn("Caractéristique");
        detailTable.AddColumn("Valeur");

        detailTable.AddRow("Nom", p.Name);
        detailTable.AddRow("Niveau", p.Level.ToString());
        detailTable.AddRow("Type(s)", p.Type2 == null ? p.Type1 : $"{p.Type1} / {p.Type2}");
        detailTable.AddRow("PV", $"{p.Health}/{p.Level * 12}");
        detailTable.AddRow("Attaque", p.Attack.ToString());
        detailTable.AddRow("Défense", p.Defense.ToString());
        detailTable.AddRow("Vitesse", p.Speed.ToString());
        detailTable.AddRow("Exp Actuelle", p.Experience.ToString());
        detailTable.AddRow("Exp Requise pour prochain niveau", missingExp.ToString());

        AnsiConsole.Write(new Panel(detailTable)
            .Header($"Résumé de {p.Name}", Justify.Center)
            .Border(BoxBorder.Double)
            .Expand());
    }

    /// <summary>
    /// Soigne tous les Pokémon du joueur.
    /// </summary>
    static void SoignerEquipe(Player player)
    {
        foreach (var pokemon in player.Pokemons)
            pokemon.Heal();

        AfficherCadre("Tous vos Pokémon sont maintenant en pleine forme !", ConsoleColor.Green);
    }

    /// <summary>
    /// Affiche les statistiques du jeu (combats, captures, etc.).
    /// </summary>
    static void AfficherStatistiques(Player player, int battleCount)
    {
        var table = new Table().Centered();
        table.AddColumn("[bold]Statistique[/]");
        table.AddColumn("[bold]Valeur[/]");

        table.AddRow("Combats effectués", battleCount.ToString());
        table.AddRow("Pokémon capturés", (player.Pokemons.Count - 1).ToString());
        table.AddRow("Pokémon dans l'équipe", player.Pokemons.Count.ToString());

        AnsiConsole.Write(new Panel(table)
            .Header("STATISTIQUES DE JEU", Justify.Center)
            .Border(BoxBorder.Rounded)
            .Expand());
    }

    /// <summary>
    /// Gère la boutique : achats de potions et Pokéballs.
    /// </summary>
    static void Boutique(Player player)
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold magenta]BIENVENUE À LA BOUTIQUE[/]")
                .AddChoices("Potion (20 pièces)", "Super Potion (50 pièces)", "Pokéball (10 pièces)", "Quitter la boutique"));

        switch (choice)
        {
            case "Potion (20 pièces)":
                player.BuyItem("Potion", 20);
                break;
            case "Super Potion (50 pièces)":
                player.BuyItem("Super Potion", 50);
                break;
            case "Pokéball (10 pièces)":
                player.BuyItem("Pokéball", 10);
                break;
            case "Quitter la boutique":
                AfficherCadre("Merci de votre visite !", ConsoleColor.Cyan);
                break;
        }
    }
}
