using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Insect
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Lumi-Queue","Insect","Support", 0, 100),
                new("Toile Gluante","Insect","Support", 0, 100),
                new("Poudre Fureur","Insect","Support", 0, 100),
                new("Papillodanse","Insect","Support", 0, 100),
                new("Sécrétion","Insect","Support", 0, 100),
            };
        }
    }
}
