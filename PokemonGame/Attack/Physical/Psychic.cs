using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Physical
{
    internal class Psychic
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Psycho Boost","Psychic","Physique", 140, 90),
                new("Psykoud'Boul","Psychic","Physique", 80, 100),
            };
        }
    }
}
