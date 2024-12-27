namespace PokemonGame.Attack.Physical;

public static class Normal
{
    public static List<AttackLogic> GetAttacks()
    {
        return new List<AttackLogic>
        {
            // Ajouter les attaques (Nom, Type, Nature, Puissance, Précision)
            new("Charge", "Normal", "Physique", 40, 100),
            new("Coup d'Boule", "Normal", "Physique", 70, 100),
            new("Écrasement", "Normal", "Physique", 65, 100),
            new("Plaquage", "Normal", "Physique", 85, 100),
            new("Vive-Attaque", "Normal", "Physique", 40, 100),
        };
    }
}