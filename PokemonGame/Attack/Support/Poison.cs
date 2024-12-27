using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Poison
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Acidarmure","Poison","Support", 0, 100),
                new("Enroulement","Poison","Support", 0, 100),
                new("Pics Toxik","Poison","Support", 0, 90),
                new("Toxik","Poison","Support", 0, 90),
                new("Poudre Toxik","Poison","Support", 0, 75),
            };
        }
    }
}
