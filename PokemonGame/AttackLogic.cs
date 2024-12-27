namespace PokemonGame;

public class AttackLogic(string name, string type, string category, int power, int accuracy, Action<Pokemon, Pokemon>? effect = null)
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
            // Calcul des multiplicateurs pour les deux types
            double multiplier1 = Pokemon.GetTypeMultiplier(Type, target.Type1);
            double multiplier2 = target.Type2 != null ? Pokemon.GetTypeMultiplier(Type, target.Type2) : 1.0;
            double totalMultiplier = multiplier1 * multiplier2;

            // Déterminer l'efficacité
            string effectiveness = Pokemon.GetEffectivenessDescription(totalMultiplier);

            // Calcul des dégâts
            int damage;
            if (Category == "Physique")
            {
                damage = (int)((2.0 * attacker.Level / 5.0 + 2) * Power * (attacker.Attack / (double)target.Defense) / 50.0 * totalMultiplier);
            }
            else if (Category == "Spéciale")
            {
                damage = (int)((2.0 * attacker.Level / 5.0 + 2) * Power * (attacker.SpecialAttack / (double)target.SpecialDefense) / 50.0 * totalMultiplier);
            }
            else
            {
                // Effet de soutien
                Effect?.Invoke(attacker, target);
                Console.WriteLine($"{attacker.Name} utilise {Name} ! {effectiveness}");
                return;
            }

            // Appliquer les dégâts
            damage = Math.Max(1, damage); // Les dégâts doivent être au minimum de 1
            target.Health -= damage;

            Console.WriteLine($"{attacker.Name} utilise {Name} ! {target.Name} perd {damage} PV ! {effectiveness}");
        }
        else
        {
            Console.WriteLine($"{attacker.Name} utilise {Name}, mais cela échoue !");
        }
    }
}
