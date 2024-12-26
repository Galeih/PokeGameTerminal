namespace PokemonGame;

public static class ExperienceCalculator
{
    /// <summary>
    /// Calcule l'XP gagnée en fonction du niveau du Pokémon vaincu avec une variation de ±5 %.
    /// </summary>
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
}
