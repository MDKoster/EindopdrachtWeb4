using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_EF.Model {
    public class TafelEF {
        public TafelEF() {
        }

        public TafelEF(int tafelNr, int aantalPlaatsen) {
            TafelNr = tafelNr;
            AantalPlaatsen = aantalPlaatsen;
        }

        public TafelEF(int iD, int tafelNr, int aantalPlaatsen) : this(iD, tafelNr) {
            AantalPlaatsen = aantalPlaatsen;
        }

        public int ID { get; set; }
        [Required]
        public int TafelNr { get; set; }
        public int AantalPlaatsen { get; set; }
        public int RestaurantID { get; set; }
        public RestaurantEF Restaurant { get; set; }
    }
}
