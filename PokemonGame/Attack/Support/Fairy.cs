using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Fairy
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Champ Brumeux","Fairy","Support", 0, 100),
                new("Charme","Fairy","Support", 0, 100),
                new("Doux Baiser","Fairy","Support", 0, 100),
                new("Rayon Lune","Fairy","Support", 0, 100),
            };
        }
    }
}
