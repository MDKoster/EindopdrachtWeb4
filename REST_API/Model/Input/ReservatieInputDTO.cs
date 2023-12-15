using RestaurantOpdracht_BL.Model;

namespace REST_API.Model.Input {
    public class ReservatieInputDTO {
        public ReservatieInputDTO(KlantInputDTO klant, RestaurantInputDTO restaurant, int aantalPlaatsen, DateTime datum, int tafelNr) {
            Klant = klant;
            Restaurant = restaurant;
            AantalPlaatsen = aantalPlaatsen;
            Datum = datum;
            TafelNr = tafelNr;
        }

        public KlantInputDTO Klant { get; set; }
        public RestaurantInputDTO Restaurant { get; set; }
        public int AantalPlaatsen { get; set; }
        public DateTime Datum { get; set; }
        public int TafelNr { get; set; }
    }
}
