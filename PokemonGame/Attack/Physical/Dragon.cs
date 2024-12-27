using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Physical
{
    public class Dragon
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Dracogriffe", "Dragon", "Physique", 80, 100),
                new("Draco-Queue", "Dragon", "Physique", 60, 90),
                new("Colère", "Dragon", "Physique", 120, 100),
                new("Draco-Charge", "Dragon", "Physique", 100, 100),
            };
        }
    }
}
