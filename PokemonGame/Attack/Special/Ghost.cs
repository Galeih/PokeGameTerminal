using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Special
{
    public class Ghost
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Ball'Ombre","Ghost","Special", 80, 100),
                new("Eclat Spectral","Ghost","Special", 120, 100),
            };
        }
    }
}
