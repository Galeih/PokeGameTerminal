using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Fire
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Rampart Brûlant","Fire","Support", 0, 100),
                new("Feu Follet","Fire","Support", 0, 85),
                new("Zénith","Fire","Support", 0, 100),
            };
        }
    }
}
