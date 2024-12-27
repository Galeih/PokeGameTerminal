using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Physical
{
    internal class Ghost
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Shadow Claw","Ghost","Physique", 70, 100),
                new("Shadow Punch","Ghost","Physique", 60, 100),
                new("Ombre Portée","Ghost","Physique", 40, 100),
                new("Hantise","Ghost","Physique", 90, 100),
            };
        }
    }
}
