namespace PokemonGame;

public class Attack(string name, string type, string category, int power, int accuracy, Action<Pokemon, Pokemon>? effect = null)
{
    public string Name { get; set; } = name;

    public string Type { get; set; } = type;

    public string Category { get; set; } = category;

    public int Power { get; set; } = power;

    public int Accuracy { get; set; } = accuracy;

    public Action<Pokemon, Pokemon>? Effect { get; set; } = effect;

    public void Use(Pokemon attacker, Pokemon target)
    {
        Random random = new();
        if (random.Next(100) < Accuracy)
        {
            if (Category == "Physique")
            {
                int damage = (int)((attacker.Attack - target.Defense / 2.0) * GetTypeMultiplier(attacker.Type1, target.Type1));
                target.Health -= Math.Max(1, damage);
                Console.WriteLine($"{attacker.Name} utilise {Name} ! {target.Name} perd {damage} PV !");
            }
            else if (Category == "Spéciale")
            {
                int damage = (int)((attacker.SpecialAttack - target.SpecialDefense / 2.0) * GetTypeMultiplier(attacker.Type1, target.Type1));
                target.Health -= Math.Max(1, damage);
                Console.WriteLine($"{attacker.Name} utilise {Name} ! {target.Name} perd {damage} PV !");
            }
            else if (Category == "Soutien")
            {
                Effect?.Invoke(attacker, target);
                Console.WriteLine($"{attacker.Name} utilise {Name} !");
            }
        }
        else
        {
            Console.WriteLine($"{attacker.Name} utilise {Name} mais l'attaque échoue !");
        }
    }

    private static double GetTypeMultiplier(string attackerType, string targetType)
    {
        // Utiliser la logique existante pour les multiplicateurs de types.
        return 1.0; // Exemple simplifié
    }
}
