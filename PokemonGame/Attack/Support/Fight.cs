using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Fight
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Gonflette","Fight","Support", 0, 100),
                new("Ultime Bastion","Fight","Support", 0, 100),
                new("Détection","Fight","Support", 0, 100),
            };
        }
    }
}
