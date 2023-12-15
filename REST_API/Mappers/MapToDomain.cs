using REST_API.Model.Input;
using RestaurantOpdracht_BL.Model;

namespace REST_API.Mappers {
    public class MapToDomain {
        public static Klant MapKlantInputToKlant(KlantInputDTO DTO) {
            Contactgegevens contactgegevens = new(DTO.Tel, DTO.Email, DTO.Postcode, DTO.Gemeentenaam, DTO.Straatnaam, DTO.HuisNr);
            return new Klant(DTO.Naam, contactgegevens);
        }

        public static Restaurant MapRestaurantInputToRestaurant(RestaurantInputDTO DTO) {
            Contactgegevens contactgegevens = new(DTO.Tel, DTO.Email, DTO.Postcode, DTO.Gemeentenaam, DTO.Straatnaam, DTO.HuisNr);
            return new Restaurant(DTO.Naam, DTO.Keuken, contactgegevens, DTO.Tafels.Select(t => MapTafelInputToTafel(t)).ToList());
        }

        public static Tafel MapTafelInputToTafel(TafelInputDTO DTO) {
            return new Tafel(DTO.TafelNr, DTO.AantalPlaatsen);
        }

        public static Reservatie MapReservatieInputToReservatie(ReservatieInputDTO DTO) {
            return new Reservatie(MapKlantInputToKlant(DTO.Klant), MapRestaurantInputToRestaurant(DTO.Restaurant), DTO.AantalPlaatsen, DTO.Datum, DTO.TafelNr);
        }
    }
}
