using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Physical
{
    internal class Ground
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Séisme","Ground","Physique", 100, 100),
                new("Tunnel","Ground","Physique", 80, 100),
                new("Piétisol","Ground","Physique", 60, 100),
            };
        }
    }
}
