using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Special
{
    public class Ice
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Blizzard","Ice","Special", 110, 70),
                new("Ice Beam","Ice","Special", 90, 100),
                new("Poudreuse","Ice","Special", 40, 100),
            };
        }
    }
}
