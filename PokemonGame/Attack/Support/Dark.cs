using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Dark
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Aiguisage","Dark","Support", 0, 100),
                new("Croco Larme","Dark","Support", 0, 100),
                new("Machination","Dark","Support", 0, 100),
                new("Dernier Mot","Dark","Support", 0, 100),
                new("Provoc","Dark","Support", 0, 100),
                new("Souvenir","Dark","Support", 0, 100),
                new("Trou Noir","Dark","Support", 0, 100),
            };
        }
    }
}
