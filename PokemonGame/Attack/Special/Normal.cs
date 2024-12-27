using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Special
{
    public class Normal
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Brouhaha","Normal","Special", 90, 100),
                new("Mégaphone","Normal","Special", 90, 100),
                new("Echo","Normal","Special", 40, 100),
                new("Ultralaser","Normal","Special", 150, 90),
                new("Bang Sonique","Normal","Special", 140, 100),
                new("Chant Canon","Normal","Special", 60, 100),
                new("Jugement","Normal","Special", 100, 100),
            };
        }
    }
}
