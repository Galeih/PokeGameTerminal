using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Physical
{
    internal class Steel
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Iron Tail","Steel","Physique", 100, 75),
                new("Metal Claw","Steel","Physique", 50, 95),
                new("Iron Head","Steel","Physique", 80, 100),
                new("Gyro Ball","Steel","Physique", 20, 100),
            };
        }
    }
}
