using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Special
{
    public class Steel
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Luminocanon","Steel","Special", 80, 100),
                new("Lame Tachyonique","Steel","Special", 50, 100),
                new("Métalaser","Steel","Special", 140, 95),
            };
        }
    }
}
