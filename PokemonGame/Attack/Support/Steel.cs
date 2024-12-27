using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Steel
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Change-Vitesse","Steel","Support", 0, 100),
                new("Mur de Fer","Steel","Support", 0, 100),
                new("Strido-Son","Steel","Support", 0, 85),
            };
        }
    }
}
