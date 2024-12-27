using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Special
{
    public class Electrik
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Éclair","Electrik","Special", 40, 100),
                new("Tonnerre","Electrik","Special", 110, 70),
                new("Fatal-Foudre","Electrik","Special", 110, 70),
                new("Rayon Chargé","Electrik","Special", 50, 90),
            };
        }
    }
}
