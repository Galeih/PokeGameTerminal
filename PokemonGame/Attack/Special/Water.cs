using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Special
{
    public class Water
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Hydrocanon","Water","Special", 110, 80),
                new("Vibraqua","Water","Special", 60, 100),
                new("Pistolet à O","Water","Special", 40, 100),
                new("Hydroblast","Water","Special", 150, 90),
                new("Surf","Water","Special", 90, 100),
            };
        }
    }
}
