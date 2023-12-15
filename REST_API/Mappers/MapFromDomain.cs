using REST_API.Model.Output;
using RestaurantOpdracht_BL.Model;

namespace REST_API.Mappers {
    public class MapFromDomain {
        public static KlantOutputDTO MapKlantToKlantOutput(Klant klant) {
            return new KlantOutputDTO(klant.ID, klant.Naam, klant.Contactgegevens.Tel, klant.Contactgegevens.Email, klant.Contactgegevens.Postcode, klant.Contactgegevens.Gemeentenaam, klant.Contactgegevens.Straatnaam, klant.Contactgegevens.HuisNr);
        }

        public static RestaurantOutputDTO MapRestaurantToRestaurantOutput(Restaurant res) {
            return new RestaurantOutputDTO(res.ID, res.Naam, res.Keuken, res.Contactgegevens.Tel, res.Contactgegevens.Email, 
                res.Contactgegevens.Postcode, res.Contactgegevens.Gemeentenaam, res.Contactgegevens.Straatnaam, 
                res.Contactgegevens.HuisNr, res.Tafels.Select(t => MapTafelToTafelOutput(t)).ToList());
        }

        public static TafelOutputDTO MapTafelToTafelOutput(Tafel tafel) {
            return new TafelOutputDTO(tafel.TafelNr, tafel.AantalPlaatsen);
        }

        public static ReservatieOutputDTO MapReservatieToReservatieOutput(Reservatie res) {
            return new ReservatieOutputDTO(res.ID, MapKlantToKlantOutput(res.Klant), MapRestaurantToRestaurantOutput(res.Restaurant),
                res.AantalPlaatsen, res.Datum, res.TafelNr);
        }
    }
}
