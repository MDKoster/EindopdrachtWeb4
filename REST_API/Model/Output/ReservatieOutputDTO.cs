using REST_API.Model.Input;

namespace REST_API.Model.Output {
    public class ReservatieOutputDTO {
        public ReservatieOutputDTO(int iD, KlantOutputDTO klant, RestaurantOutputDTO restaurant, int aantalPlaatsen, DateTime datum, int tafelNr) {
            ID = iD;
            Klant = klant;
            Restaurant = restaurant;
            AantalPlaatsen = aantalPlaatsen;
            Datum = datum;
            TafelNr = tafelNr;
        }

        public int ID { get; set; }
        public KlantOutputDTO Klant { get; set; }
        public RestaurantOutputDTO Restaurant { get; set; }
        public int AantalPlaatsen { get; set; }
        public DateTime Datum { get; set; }
        public int TafelNr { get; set; }
    }
}
