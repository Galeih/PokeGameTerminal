using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Ice
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Brume","Ice","Support", 0, 100),
                new("Buée Noire","Ice","Support", 0, 100),
                new("Neigeux de Mots","Ice","Support", 0, 100),
                new("Voile Aurore","Ice","Support", 0, 100),
            };
        }
    }
}
