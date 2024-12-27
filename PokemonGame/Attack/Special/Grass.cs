using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Special
{
    public class Grass
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Vol-Vie","Grass","Special", 40, 100),
                new("Mégasangsue","Grass","Special", 75, 100),
                new("Giga-Sangsue","Grass","Special", 75, 100),
                new("Tempête verte","Grass","Special", 130, 90),
                new("Lance-Soleil","Grass","Special", 120, 100),
                new("Végé-Attak","Grass","Special", 150, 90),
                new("Danse-Fleur","Grass","Special", 120, 100),
                new("Feuille Magik","Grass","Special", 60, 100),
            };
        }
    }
}
