﻿namespace PokemonGame;

public class Battle
{
    private Player Player1;
    private Player Player2;

    public Battle(Player player1, Player player2)
    {
        Player1 = player1;
        Player2 = player2;
    }

    public void StartBattle()
    {
        Pokemon attacker = Player1.Pokemons[0];
        Pokemon defender = Player2.Pokemons[0];

        while (!attacker.IsFainted() && !defender.IsFainted())
        {
            Console.WriteLine($"{attacker.Name} (PV : {attacker.Health}) VS {defender.Name} (PV : {defender.Health})");
            Console.WriteLine("Choisissez une attaque :");
            for (int i = 0; i < attacker.Moves.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {attacker.Moves[i].Name}");
            }

            int choice = int.Parse(Console.ReadLine() ?? "1") - 1;
            if (choice >= 0 && choice < attacker.Moves.Count)
            {
                attacker.Moves[choice].Use(attacker, defender);
            }

            // Inverse les rôles pour continuer le combat
            Pokemon temp = attacker;
            attacker = defender;
            defender = temp;
        }

        if (attacker.IsFainted())
        {
            Console.WriteLine($"{attacker.Name} est K.O. !");
        }
        else if (defender.IsFainted())
        {
            Console.WriteLine($"{defender.Name} est K.O. !");
        }
    }

    public void StartWithHealthBar()
    {
        Pokemon p1 = Player1.Pokemons[0];
        Pokemon p2 = Player2.Pokemons[0];

        Console.WriteLine("\nLe combat commence !");
        while (!p1.IsFainted() && !p2.IsFainted())
        {
            AfficherBarreDeVie(p1, p2);

            p1.AttackPokemon(p2);
            if (p2.IsFainted())
            {
                Console.WriteLine($"{p2.Name} est K.O !");

                // Calcul de l'XP gagnée
                int expGagnee = ExperienceCalculator.CalculerExperienceGagnee(p2.Level);
                Console.WriteLine($"{p1.Name} gagne {expGagnee} points d'expérience !");

                // Ajout de l'XP au Pokémon victorieux
                p1.GainExperience(expGagnee);

                break;
            }

            p2.AttackPokemon(p1);
            if (p1.IsFainted())
            {
                Console.WriteLine($"{p1.Name} est K.O !");
                break;
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
        const int totalBarres = 20; // Nombre de segments dans la barre de vie
        int barresRemplies = (int)((current / (double)max) * totalBarres);
        int barresVides = totalBarres - barresRemplies;

        // Couleur de la barre de vie
        string color = current > max * 0.5 ? "green" : current > max * 0.2 ? "yellow" : "red";

        // Barre remplie et vide
        string filledBar = new string('█', barresRemplies);
        string emptyBar = new string('░', barresVides);

        // Retourne la barre de vie colorée
        return $"[bold {color}]{filledBar}[/]{emptyBar}";
    }

}
