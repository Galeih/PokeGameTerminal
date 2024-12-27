using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Special
{
    public class Fight
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Exploforce","Fight","Special", 120, 70),
                new("Aurasphère","Fight","Special", 80, 100),
                new("Onde Vide","Fight","Special", 40, 100),
            };
        }
    }
}
