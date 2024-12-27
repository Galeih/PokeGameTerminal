using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Physical
{
    internal class Rock
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Éboulement","Rock","Physique", 75, 90),
                new("Roc-Boulet","Rock","Physique", 90, 90),
                new("Roulade","Rock","Physique", 30, 90),
                new("Jet-Pierres","Rock","Physique", 50, 90),
                new("Tomberoche","Rock","Physique", 60, 95),
            };
        }
    }
}
