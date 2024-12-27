using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Dragon
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Danse Draco","Dragon","Support", 0, 100),
                new("Cri Draconique","Dragon","Support", 0, 100),
            };
        }
    }
}
