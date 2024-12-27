namespace PokemonGame;

public static class AttackData
{
    public static List<Attack> GetAllAttacks()
    {
        return new List<Attack>
        {
            new("Lance-Flammes", "Feu", "Spéciale", 90, 100),
            new("Charge", "Normal", "Physique", 40, 100),
            new("Rugissement", "Normal", "Soutien", 0, 100, (attacker, target) =>
            {
                target.Attack -= 2;
                Console.WriteLine($"{target.Name} voit son attaque baisser !");
            }),
            new("Cage-Éclair", "Électrique", "Soutien", 0, 90, (attacker, target) =>
            {
                Console.WriteLine($"{target.Name} est paralysé !");
                target.Speed /= 2;
            })
        };
    }
}
