using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Physical
{
    internal class Flying
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Fly","Flying","Physique", 90, 95),
                new("Brave Bird","Flying","Physique", 120, 100),
                new("Sky Attack","Flying","Physique", 140, 90),
                new("Aerial Ace","Flying","Physique", 60, 999),
            };
        }
    }
}
