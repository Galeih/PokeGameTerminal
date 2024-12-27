using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Psychic
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Amnésie","Psychic","Support", 0, 100),
                new("Champ Psychique","Psychic","Support", 0, 100),
                new("Hypnose","Psychic","Support", 0, 60),
                new("Mur Lumière","Psychic","Support", 0, 100),
                new("Protection","Psychic","Support", 0, 100),
                new("Partage Force","Psychic","Support", 0, 100),
                new("Partage Garde","Psychic","Support", 0, 100),
                new("Permugarde","Psychic","Support", 0, 100),
                new("Permuforce","Psychic","Support", 0, 100),
                new("Plénitude","Psychic","Support", 0, 100),
                new("Poudre Magique","Psychic","Support", 0, 100),
                new("Repos","Psychic","Support", 0, 100),
                new("Téléport","Psychic","Support", 0, 100),
                new("Tour de Magie","Psychic","Support", 0, 100),
            };
        }
    }
}
