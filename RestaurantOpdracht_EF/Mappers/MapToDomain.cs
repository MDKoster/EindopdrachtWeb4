using RestaurantOpdracht_BL.Model;
using RestaurantOpdracht_EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_EF.Mappers {
    internal class MapToDomain {
        internal static Klant MapKlantEFToKlant(KlantEF klantEF) {
            Contactgegevens contactgegevens = new(klantEF.Tel, klantEF.Email, klantEF.Postcode, klantEF.Gemeentenaam, klantEF.Straatnaam, klantEF.HuisNr);
            return new Klant(klantEF.ID, klantEF.Naam, contactgegevens);
        }

        internal static Reservatie MapReservatieEFToReservatie(ReservatieEF r) {
            return new Reservatie(r.ID, MapKlantEFToKlant(r.Klant), MapRestaurantEFToRestaurant(r.Restaurant), r.AantalPlaatsen, r.Datum, r.TafelNr);
        }

        internal static Restaurant MapRestaurantEFToRestaurant(RestaurantEF res) {
            Contactgegevens contactgegevens = new(res.Tel, res.Email, res.Postcode, res.Gemeentenaam, res.Straatnaam, res.HuisNr);
            return new Restaurant(res.ID, res.Naam, res.Keuken, contactgegevens, res.Tafels.Select<TafelEF, Tafel>(t => MapTafelEFToTafel(t)).ToList());
        }

        internal static Tafel MapTafelEFToTafel(TafelEF tafel) {
            return new Tafel(tafel.TafelNr, tafel.AantalPlaatsen);
        }
    }
}
