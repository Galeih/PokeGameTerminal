using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Physical
{
    internal class Ice
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Ice Punch","Ice","Physique", 75, 100),
                new("Ice Fang","Ice","Physique", 65, 95),
                new("Ice Shard","Ice","Physique", 40, 100),
            };
        }
    }
}
