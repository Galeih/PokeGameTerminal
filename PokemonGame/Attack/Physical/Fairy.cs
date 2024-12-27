namespace PokemonGame.Attack.Physical;

internal class Fairy
{
    public static List<AttackLogic> GetAttacks()
    {
        return new List<AttackLogic>
        {
            new("PlayRougth","Fairy","Physique", 90, 90),
            new("Misty-Explosion","Fairy","Physique", 100, 100, (attacker, target) =>
            {
                attacker.Health = 0;
                target.Health = 0;
                Console.WriteLine($"{attacker.Name} utilise {attacker.AttackLogic.Name} et explose !");
            }),
        };
    }
}
