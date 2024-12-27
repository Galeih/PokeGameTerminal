using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Special
{
    public class Insect
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Vampirisme","Insect","Special", 80, 100),
                new("Bourdon","Insect","Special", 90, 100),
                new("Vent Argenté","Insect","Special", 60, 100),
            };
        }
    }
}
