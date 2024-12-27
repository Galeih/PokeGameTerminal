using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Special
{
    public class Dragon
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Draco Météor","Dragon","Special", 130, 90),
                new("Dracochoc","Dragon","Special", 85, 100),
                new("Dracosouffle","Dragon","Special", 60, 100),
            };
        }
    }
}
