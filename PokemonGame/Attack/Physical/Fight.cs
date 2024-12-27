using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Physical
{
    internal class Fight
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Poing-Karaté","Fight","Physique", 50, 100),
                new("Double-Pied","Fight","Physique", 30, 100),
                new("Balayage","Fight","Physique", 40, 100),
                new("Coup-Croix","Fight","Physique", 100, 80),
                new("Close Combat","Fight","Physique", 120, 100),
                new("Mach Punch","Fight","Physique", 40, 100),
                new("Balayette","Fight","Physique", 60, 100),
                new("Dynamopoing","Fight","Physique", 100, 50),
                new("Casse-Brique","Fight","Physique", 75, 100),
            };
        }
    }
}
