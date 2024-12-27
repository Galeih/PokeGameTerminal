using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Attack.Support
{
    public class Normal
    {
        public static List<AttackLogic> GetAttacks()
        {
            return new List<AttackLogic>
            {
                new("Abri","Normal","Support", 0, 100),
                new("Accupression","Normal","Support", 0, 100),
                new("Armure","Normal","Support", 0, 100),
                new("Boul'Armure","Normal","Support", 0, 100),
                new("Clonage","Normal","Support", 0, 100),
                new("Coup d'Main","Normal","Support", 0, 100),
                new("Danse-Lames","Normal","Support", 0, 100),
                new("E-Coque","Normal","Support", 0, 100),
                new("Encore","Normal","Support", 0, 100),
                new("Exuviation","Normal","Support", 0, 100),
                new("Entrave","Normal","Support", 0, 100),
                new("Gribouille","Normal","Support", 0, 100),
                new("Gros Yeux","Normal","Support", 0, 100),
                new("Hurlement","Normal","Support", 0, 100),
                new("Lait à Boire","Normal","Support", 0, 100),
                new("Métronome","Normal","Support", 0, 100),
                new("Morphing","Normal","Support", 0, 100),
                new("Paresse","Normal","Support", 0, 100),
                new("Queulonage","Normal","Support", 0, 100),
                new("Rugissement","Normal","Support", 0, 100),
                new("Relais","Normal","Support", 0, 100),
                new("Regard Médusant","Normal","Support", 0, 100),
                new("Rengorgement","Normal","Support", 0, 100),
                new("Regard Noir","Normal","Support", 0, 100),
                new("Soin","Normal","Support", 0, 100),
                new("Trempette","Normal","Support", 0, 100),
                new("Voeu","Normal","Support", 0, 100),
            };
        }
    }
}
