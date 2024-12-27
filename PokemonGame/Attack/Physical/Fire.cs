using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Physical
{
    internal class Fire
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Fire Punch","Fire","Physique", 75, 100),
                new("Fire Fang","Fire","Physique", 65, 95),
                new("Flare Blitz","Fire","Physique", 120, 100),
                new("Blaze Kick","Fire","Physique", 85, 90),
            };
        }
    }
}
