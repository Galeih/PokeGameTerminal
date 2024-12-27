using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Physical
{
    public class Electrik
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Spark","Electrik","Physique", 65, 100),
                new("Wild Charge","Electrik","Physique", 90, 100),
                new("Volt Tackle","Electrik","Physique", 120, 100),
                new("Thunder Fang","Electrik","Physique", 65, 95),
                new("Thunder Punch","Electrik","Physique", 75, 100),
            };
        }
    }
}
