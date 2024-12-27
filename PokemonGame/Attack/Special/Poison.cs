using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Special
{
    public class Poison
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Bomb-Beurk","Poison","Special", 90, 100),
                new("Acide","Poison","Special", 40, 100),
                new("Bombe Acide","Poison","Special", 40, 100),
                new("Cradovague","Poison","Special", 95, 100),
            };
        }
    }
}
