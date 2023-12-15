using RestaurantOpdracht_BL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_EF.Model {
    public class ReservatieEF {
        public ReservatieEF() {
        }

        public ReservatieEF(KlantEF klant, RestaurantEF restaurant, int aantalPlaatsen, DateTime datum, int tafelNr) {
            Klant = klant;
            Restaurant = restaurant;
            AantalPlaatsen = aantalPlaatsen;
            Datum = datum;
            TafelNr = tafelNr;
        }

        public ReservatieEF(int iD, KlantEF klant, RestaurantEF restaurant, int aantalPlaatsen, DateTime datum, int tafelNr) : this(klant, restaurant, aantalPlaatsen, datum, tafelNr) {
            ID = iD;
        }

        public int ID { get; set; }
        public int KlantID { get; set; }
        [Required]
        public KlantEF Klant { get; set; }
        public int RestaurantID { get; set; }
        [Required]
        public RestaurantEF Restaurant { get; set; }
        [Required]
        public int AantalPlaatsen { get; set; }
        [Required]
        public DateTime Datum { get; set; }
        [Required]
        public int TafelNr { get; set; }
    }
}
