using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Physical
{
    internal class Poison
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Détricanon","Poison","Physique", 120, 85),
                new("Direct Toxik","Poison","Physique", 80, 100),
                new("Dard-Venin","Poison","Physique", 15, 100),
            };
        }
    }
}
