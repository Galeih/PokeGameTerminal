using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Special
{
    public class Ground
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Telluriforce","Ground","Special", 90, 100),
                new("Coud'Boue","Ground","Special", 20, 100),
                new("Boue-Bombe","Ground","Special", 65, 85),
            };
        }
    }
}
