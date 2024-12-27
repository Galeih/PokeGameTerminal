using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Special
{
    public class Flying
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Tranch'Air","Flying","Special", 75, 95),
                new("Tornade","Flying","Special", 40, 100),
                new("Vent Violent","Flying","Special", 120, 70),
            };
        }
    }
}
