using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_BL.Model {
    public class Tafel {
        public Tafel(int tafelNr, int aantalPlaatsen) {
            TafelNr = tafelNr;
            AantalPlaatsen = aantalPlaatsen;
        }

        public int TafelNr { get; set; }
        public int AantalPlaatsen { get; set; }
    }
}
