namespace PokemonGame;

public class Battle
{
    private Player Player1;
    private Player Player2;

    public Battle(Player player1, Player player2)
    {
        Player1 = player1;
        Player2 = player2;
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
        Console.WriteLine("╔═══════════════════════════════════════╗");
        Console.WriteLine($"║ {p1.Name} PV: [{GetBarreDeVie(p1.Health, p1.Level * 12)}]");
        Console.WriteLine($"║ {p2.Name} PV: [{GetBarreDeVie(p2.Health, p2.Level * 12)}]");
        Console.WriteLine("╚═══════════════════════════════════════╝");
    }

    private string GetBarreDeVie(int current, int max)
    {
        const int totalBarres = 20;
        int barresRemplies = (int)((current / (double)max) * totalBarres);
        return new string('█', barresRemplies).PadRight(totalBarres, ' ');
    }
}
