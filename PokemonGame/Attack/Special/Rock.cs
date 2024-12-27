using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Special
{
    public class Rock
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Pouvoir Antique","Rock","Special", 60, 100),
                new("Laser Météore","Rock","Special", 120, 90),
                new("Rayon Gemme","Rock","Special", 80, 100),
            };
        }
    }
}
