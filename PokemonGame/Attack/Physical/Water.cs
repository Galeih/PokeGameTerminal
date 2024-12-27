using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Physical
{
    internal class Water
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Aqua Jet","Water","Physique", 40, 100),
                new("Cascade","Water","Physique", 80, 100),
                new("Hydroqueue","Water","Physique", 90, 90),
            };
        }
    }
}
