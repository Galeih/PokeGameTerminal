using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Physical
{
    public class Dark
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Morsure", "Dark", "Physique", 60, 100),
                new("Tricherie", "Dark", "Physique", 95, 100),
                new("Représailles", "Dark", "Physique", 50, 100, (attacker, target) => attacker.Attack *= 2),
            };
        }
    }
}
