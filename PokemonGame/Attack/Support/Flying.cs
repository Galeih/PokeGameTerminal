using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Flying
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Anti-Brume","Flying","Support", 0, 100),
                new("Vent Arrière","Flying","Support", 0, 100),
                new("Atterrissage","Flying","Support", 0, 100),
            };
        }
    }
}
