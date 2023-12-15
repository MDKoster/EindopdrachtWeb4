using RestaurantOpdracht_BL.Model;
using RestaurantOpdracht_EF.Model;

namespace RestaurantOpdracht_EF.Mappers {
    internal class MapFromDomain {
        public static KlantEF MapKlantToKlantEF(Klant klant, RestaurantContext ctx) {
            KlantEF klantEF = new KlantEF(klant.Naam, klant.Contactgegevens.Tel, klant.Contactgegevens.Email, klant.Contactgegevens.Postcode, klant.Contactgegevens.Gemeentenaam, klant.Contactgegevens.Straatnaam, klant.Contactgegevens.HuisNr);
            if (klant.ID > 0) klantEF.ID = klant.ID;
            return klantEF;
        }

        public static RestaurantEF MapRestaurantToRestaurantEF(Restaurant res, RestaurantContext ctx) {
            RestaurantEF restaurantEF = new RestaurantEF(res.Naam, res.Keuken, res.Contactgegevens.Tel, res.Contactgegevens.Email,
                res.Contactgegevens.Postcode, res.Contactgegevens.Gemeentenaam, res.Contactgegevens.Straatnaam,
                res.Contactgegevens.HuisNr, res.Tafels.Select(t => MapTafelToTafelEF(t)).ToList(), true);
            if (res.ID > 0) restaurantEF.ID = res.ID;
            return restaurantEF;
        }

        public static TafelEF MapTafelToTafelEF(Tafel tafel) {
            return new TafelEF(tafel.TafelNr, tafel.AantalPlaatsen);
        }

        internal static ReservatieEF MapReservatieToReservatieEF(Reservatie reservatie, RestaurantContext ctx) {
            ReservatieEF reservatieEF = new ReservatieEF(MapKlantToKlantEF(reservatie.Klant, ctx), MapRestaurantToRestaurantEF(reservatie.Restaurant, ctx), reservatie.AantalPlaatsen, reservatie.Datum, reservatie.TafelNr);
            return reservatieEF;
        }
    }
}
