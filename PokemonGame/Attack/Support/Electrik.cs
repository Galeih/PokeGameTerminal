using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Electrik
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Cage-Éclair","Electrik","Support", 0, 100),
                new("Chargeur","Electrik","Support", 0, 100),
                new("Champ Électrifié","Electrik","Support", 0, 100),
                new("Vol Magnétik","Electrik","Support", 0, 100),
            };
        }
    }
}
