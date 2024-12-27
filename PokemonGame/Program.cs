using PokemonGame;
using Spectre.Console;

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

        // Dialogues pour la sélection des starters
        AfficherDialogueSansEcart($"Prof. Chen : Bienvenue, [bold yellow]{playerName}[/] !");
        AfficherDialogueSansEcart("Prof. Chen : Aujourd'hui, tu vas commencer ton aventure Pokémon !");
        AfficherDialogueSansEcart("Prof. Chen : Je vais te confier un Pokémon qui deviendra ton partenaire.");
        AfficherDialogueSansEcart("Prof. Chen : Fais ton choix parmi les quatre Pokémon disponibles !");

        // Sélection du Pokémon de départ
        AnsiConsole.MarkupLine("");
        string starterChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold cyan]Quel Pokémon veux-tu choisir ?[/]")
                .AddChoices(
                    "Bulbizarre 🍃 (Type : Plante/Poison)",
                    "Salamèche 🔥 (Type : Feu)",
                    "Carapuce 🌊 (Type : Eau)",
                    "Pikachu ⚡ (Type : Électrique)")
        );

        // Créer le starter choisi
        Pokemon starter = starterChoice switch
        {
            "Bulbizarre 🍃 (Type : Plante/Poison)" => new Pokemon("Bulbizarre", 5, 45, 49, "Plante", "Poison", 45),
            "Salamèche 🔥 (Type : Feu)" => new Pokemon("Salamèche", 5, 39, 52, "Feu", null, 39),
            "Carapuce 🌊 (Type : Eau)" => new Pokemon("Carapuce", 5, 44, 48, "Eau", null, 44),
            "Pikachu ⚡ (Type : Électrique)" => new Pokemon("Pikachu", 5, 35, 55, "Électrique", null, 190),
            _ => throw new InvalidOperationException()
        };

        // Définir les PV maximaux dès la création
        starter.Health = starter.Level * 12;

        // Ajout des attaques spécifiques
        AjouterAttaquesInitiales(starter);
        AnsiConsole.MarkupLine(""); // Écart après les attaques

        // Confirmation avec dialogue
        AfficherDialogueSansEcart($"Prof. Chen : Très bon choix, [bold yellow]{playerName}[/] !");
        AfficherDialogueSansEcart($"Prof. Chen : [bold green]{starter.Name}[/] sera ton partenaire pour cette aventure.");
        AfficherDialogueSansEcart("Prof. Chen : Prends soin de lui et explore le monde des Pokémon !");
        AnsiConsole.MarkupLine("");
        AnsiConsole.MarkupLine($"🎉 [bold green]{starter.Name}[/] rejoint votre équipe avec tous ses PV complets !");
        AnsiConsole.MarkupLine("");
        player.AddPokemon(starter);

        // Boucle principale : menu du jeu
        while (true)
        {
            // Menu principal
            AnsiConsole.MarkupLine("");
            string action = AnsiConsole.Prompt(
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
                    AfficherCadre("Merci d'avoir joué à Pokémon Rouge ! À bientôt !", ConsoleColor.Red);
                    Console.ReadKey();
                    return;
            }
        }
    }

    static void AfficherBanniere()
    {
        Console.Clear();

        // Titre principal avec effet de style
        AnsiConsole.Write(
            new FigletText("POKÉMON ROUGE")
                .Centered()
                .Color(Color.Red));

        AnsiConsole.MarkupLine("");

        // Création d'une table dynamique avec options interactives
        var menuTable = new Table()
            .Centered()
            .AddColumn(new TableColumn("[bold yellow]BIENVENUE À KANTO ![/]").Centered());

        menuTable.AddRow(new Markup("[bold cyan]> Nouvelle Partie[/]"));
        menuTable.AddRow(new Markup("[bold cyan]> Charger Partie[/]"));
        menuTable.AddRow(new Markup("[bold cyan]> Options[/]"));
        menuTable.AddRow(new Markup("[bold cyan]> Quitter[/]"));
        menuTable.Border(TableBorder.DoubleEdge);

        // Afficher la table
        AnsiConsole.Write(menuTable);

        AnsiConsole.MarkupLine("");
        AnsiConsole.MarkupLine("[bold green]Utilisez les flèches pour naviguer et appuyez sur Entrée pour sélectionner une option.[/]");
        AnsiConsole.MarkupLine("");

        // Menu interactif basé sur les choix dans la table
        var choix = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold green]Menu Principal[/]")
                .AddChoices("Nouvelle Partie", "Charger Partie", "Options", "Quitter"));

        // Gérer les choix de l'utilisateur
        switch (choix)
        {
            case "Nouvelle Partie":
                AnsiConsole.MarkupLine("[bold cyan]Lancement d'une nouvelle aventure ![/]");
                break;

            case "Charger Partie":
                AnsiConsole.MarkupLine("[bold cyan]Chargement d'une partie existante ![/]");
                break;

            case "Options":
                AnsiConsole.MarkupLine("[bold cyan]Ouverture des options ![/]");
                break;

            case "Quitter":
                AnsiConsole.MarkupLine("[bold cyan]Merci d'avoir joué à POKÉMON ROUGE ![/]");
                Environment.Exit(0);
                break;
        }

        Console.Clear();
    }

    static void SimulerChargement(string message)
    {
        AnsiConsole.MarkupLine("");
        AnsiConsole.Status()
            .Start(message, ctx =>
            {
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(50);
                }
            });
        AnsiConsole.MarkupLine("");
    }

    static void AfficherCadre(string message, ConsoleColor color = ConsoleColor.Yellow)
    {
        AnsiConsole.MarkupLine("");
        AnsiConsole.Write(
            new Panel($"[bold {color.ToString().ToLower()}]{message}[/]")
                .Border(BoxBorder.Rounded)
                .Padding(1, 1)
                .Expand()
        );
        AnsiConsole.MarkupLine("");
    }

    static void AfficherDialogueSansEcart(string message)
    {
        AnsiConsole.Write(
            new Panel($"[bold white]{message}[/]")
                .Header("[bold yellow]Dialogue[/]")
                .Border(BoxBorder.Rounded)
                .Padding(1, 1)
                .Expand()
        );
        Console.ReadKey(true); // Pause pour simuler un dialogue interactif
    }

    static void AjouterAttaquesInitiales(Pokemon starter)
    {
        switch (starter.Name)
        {
            case "Bulbizarre":
                starter.LearnMove(new AttackLogic("Fouet Lianes", "Plante", "Physique", 45, 100));
                starter.LearnMove(new AttackLogic("Charge", "Normal", "Physique", 40, 100));
                starter.LearnMove(new AttackLogic("Rugissement", "Normal", "Soutien", 0, 100));
                starter.LearnMove(new AttackLogic("Poudre Dodo", "Plante", "Soutien", 0, 75, (attacker, target) =>
                {
                    target.Status = "Endormi";
                    Console.WriteLine($"{target.Name} s'endort !");
                }));
                break;

            case "Salamèche":
                starter.LearnMove(new AttackLogic("Griffe", "Normal", "Physique", 40, 100));
                starter.LearnMove(new AttackLogic("Flammèche", "Feu", "Spéciale", 40, 100));
                starter.LearnMove(new AttackLogic("Rugissement", "Normal", "Soutien", 0, 100));
                starter.LearnMove(new AttackLogic("Grondement", "Normal", "Soutien", 0, 100));
                break;

            case "Carapuce":
                starter.LearnMove(new AttackLogic("Charge", "Normal", "Physique", 40, 100));
                starter.LearnMove(new AttackLogic("Pistolet à O", "Eau", "Spéciale", 40, 100));
                starter.LearnMove(new AttackLogic("Rugissement", "Normal", "Soutien", 0, 100));
                starter.LearnMove(new AttackLogic("Protection", "Normal", "Soutien", 0, 100));
                break;

            case "Pikachu":
                starter.LearnMove(new AttackLogic("Charge", "Normal", "Physique", 40, 100));
                starter.LearnMove(new AttackLogic("Éclair", "Électrique", "Spéciale", 40, 100));
                starter.LearnMove(new AttackLogic("Vive-Attaque", "Normal", "Physique", 40, 100));
                starter.LearnMove(new AttackLogic("Cage-Éclair", "Électrique", "Soutien", 0, 90, (attacker, target) =>
                {
                    target.Status = "Paralysé";
                    Console.WriteLine($"{target.Name} est paralysé !");
                }));
                break;
        }
    }

    static void CombatSauvage(ref int battleCount, Player player)
    {
        AnsiConsole.MarkupLine("");
        string zoneName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold green]Choisissez une zone pour explorer :[/]")
                .AddChoices("Forêt", "Montagne", "Lac", "Grotte", "Plaine", "Zone industrielle", "Zone rare"));

        Pokemon wildPokemon = WildPokemonData.GenerateWildPokemon(player.ZoneLevel, zoneName);

        wildPokemon.LearnMove(new AttackLogic("Charge", "Normal", "Physique", 40, 100));
        wildPokemon.LearnMove(new AttackLogic("Morsure", "Ténèbres", "Physique", 60, 100));

        AfficherCadre($"Un [bold yellow]{wildPokemon.Name}[/] sauvage apparaît dans la zone {zoneName} !", ConsoleColor.Blue);

        Battle battle = new(player, wildPokemon);
        battle.StartBattle();

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

    static void VoirEquipeOuResume(Player player)
    {
        // Affichage de l'équipe
        AfficherEquipe(player);

        // Proposer de voir un résumé spécifique
        if (AnsiConsole.Confirm("Voulez-vous consulter le résumé d'un Pokémon ?"))
        {
            // Sélection du Pokémon à voir
            string pkmnChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold blue]Choisissez un Pokémon pour voir son résumé :[/]")
                    .AddChoices(player.Pokemons.Select(pk => pk.Name))
            );

            Pokemon? chosenPokemon = player.Pokemons.FirstOrDefault(pk => pk.Name == pkmnChoice);
            if (chosenPokemon != null)
            {
                AfficherResumePokemon(chosenPokemon);
            }
        }
    }

    static void SoignerEquipe(Player player)
    {
        foreach (Pokemon pokemon in player.Pokemons)
        {
            pokemon.Heal();
        }

        AfficherCadre("Tous vos Pokémon sont maintenant en pleine forme !", ConsoleColor.Green);
    }

    static void AfficherStatistiques(Player player, int battleCount)
    {
        Table table = new Table().Centered();
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

    static void Boutique(Player player)
    {
        string choice = AnsiConsole.Prompt(
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

    static void AfficherEquipe(Player player)
    {
        Table table = new Table().Centered();
        table.AddColumn("[bold]Pokémon[/]");
        table.AddColumn("[bold]Niveau[/]");
        table.AddColumn("[bold]PV[/]");
        table.AddColumn("[bold]Attaque[/]");
        table.AddColumn("[bold]Type 1[/]");
        table.AddColumn("[bold]Type 2[/]");

        foreach (Pokemon pokemon in player.Pokemons)
        {
            table.AddRow(
                pokemon.Name,
                pokemon.Level.ToString(),
                $"{pokemon.Health}/{pokemon.Level * 12}",
                pokemon.Attack.ToString(),
                pokemon.Type1,
                pokemon.Type2 ?? "-"
            );
        }

        AnsiConsole.Write(new Panel(table)
            .Header("ÉQUIPE POKÉMON", Justify.Center)
            .Border(BoxBorder.Rounded)
            .Expand());
    }

    static void AfficherResumePokemon(Pokemon pokemon)
    {
        Table table = new Table().Centered();
        table.AddColumn("[bold]Statistique[/]");
        table.AddColumn("[bold]Valeur[/]");

        table.AddRow("Nom", pokemon.Name);
        table.AddRow("Niveau", pokemon.Level.ToString());
        table.AddRow("PV", $"{pokemon.Health}/{pokemon.Level * 12}");
        table.AddRow("Attaque", pokemon.Attack.ToString());
        table.AddRow("Défense", pokemon.Defense.ToString());
        table.AddRow("Attaque Spéciale", pokemon.SpecialAttack.ToString());
        table.AddRow("Défense Spéciale", pokemon.SpecialDefense.ToString());
        table.AddRow("Vitesse", pokemon.Speed.ToString());
        table.AddRow("Type 1", pokemon.Type1);
        table.AddRow("Type 2", pokemon.Type2 ?? "-");

        AnsiConsole.Write(new Panel(table)
            .Header($"Résumé de {pokemon.Name}", Justify.Center)
            .Border(BoxBorder.Rounded)
            .Expand());
    }
}
