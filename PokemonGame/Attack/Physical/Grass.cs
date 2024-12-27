using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Physical
{
    internal class Grass
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Tranch'Herbe","Grass","Physique", 55, 95),
                new("Mégafouet","Grass","Physique", 120, 85),
                new("Canon Graine","Grass","Physique", 80, 100),
                new("Fouet Lianes","Grass","Physique", 45, 100),
            };
        }
    }
}
