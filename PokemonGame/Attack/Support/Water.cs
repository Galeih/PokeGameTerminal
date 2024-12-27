using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Water
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Anneau Hydro","Water","Support", 0, 100),
                new("Danse Pluie","Water","Support", 0, 100),
                new("Fontaine de Vie","Water","Support", 0, 100),
                new("Détrempage","Water","Support", 0, 100),
            };
        }
    }
}
