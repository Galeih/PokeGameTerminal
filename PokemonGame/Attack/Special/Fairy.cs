using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Special
{
    public class Fairy
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Eclat Magique","Fairy","Special", 80, 100),
                new("Pouvoir Lunaire","Fairy","Special", 95, 100),
                new("Vampibaiser","Fairy","Special", 50, 100),
            };
        }
    }
}
