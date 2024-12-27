using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Ghost
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Dépit","Ghost","Support", 0, 100),
                new("Destiny Bond","Ghost","Support", 0, 100),
                new("Malédiction","Ghost","Support", 0, 100),
                new("Onde Folie","Ghost","Support", 0, 100),
            };
        }
    }
}
