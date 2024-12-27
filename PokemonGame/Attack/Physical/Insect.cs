using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Physical
{
    internal class Insect
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Piqûre","Insect","Physique", 60, 100),
                new("Plaie-Croix","Insect","Physique", 80, 100),
                new("Mégacorne","Insect","Physique", 120, 85),
                new("Taillade","Insect","Physique", 40, 100),
            };
        }
    }
}
