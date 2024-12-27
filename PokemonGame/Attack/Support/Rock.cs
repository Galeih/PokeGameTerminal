using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Rock
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Garde Large","Rock","Support", 0, 100),
                new("Piège de Roc","Rock","Support", 0, 100),
                new("Poliroche","Rock","Support", 0, 100),
                new("Tempête de Sable","Rock","Support", 0, 100),
            };
        }
    }
}
