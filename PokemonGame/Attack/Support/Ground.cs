using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Ground
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Jet de Sable","Ground","Support", 0, 100),
                new("Amass'Sable","Ground","Support", 0, 100),
                new("Picots","Ground","Support", 0, 100),
            };
        }
    }
}
