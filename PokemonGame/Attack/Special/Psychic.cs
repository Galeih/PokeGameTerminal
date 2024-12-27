using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Special
{
    public class Psychic
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Choc Mental","Psychic","Special", 50, 100),
                new("Psyko","Psychic","Special", 90, 100),
                new("Rafale Psy","Psychic","Special", 65, 100),
            };
        }
    }
}
