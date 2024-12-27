using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Special
{
    public class Fire
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Lance-Flamme","Fire","Special", 90, 100),
                new("Déflagration","Fire","Special", 110, 85),
                new("Surchauffe","Fire","Special", 130, 90),
                new("Flamme Bleue","Fire","Special", 130, 85),
            };
        }
    }
}
