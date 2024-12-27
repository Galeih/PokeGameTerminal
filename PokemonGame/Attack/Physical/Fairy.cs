namespace PokemonGame.Attack.Physical;

internal class Fairy
{
    public static List<AttackLogic> GetAttacks()
    {
        return new List<AttackLogic>
        {
            new("PlayRough", "Fairy", "Physique", 90, 90),
            new("Misty-Explosion", "Fairy", "Physique", 100, 100, (attacker, target) =>
            {
                Console.WriteLine($"{attacker.Name} utilise Misty-Explosion et explose !");
                attacker.Health = 0;
                target.Health = 0;
            }),
        };
    }
}
