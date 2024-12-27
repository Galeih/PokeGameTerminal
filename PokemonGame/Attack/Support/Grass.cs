using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Grass
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Vampigraine","Grass","Support", 0, 90),
                new("Cotogarde","Grass","Support", 0, 100),
                new("Champ Herbu","Grass","Support", 0, 100),
                new("Pico-Défense","Grass","Support", 0, 100),
                new("Para-Spore","Grass","Support", 0, 75),
                new("Spore","Grass","Support", 0, 100),
                new("Poudre Dodo","Grass","Support", 0, 75),
                new("Vole Force","Grass","Support", 0, 100),
                new("Synthèse","Grass","Support", 0, 100),
            };
        }
    }
}
