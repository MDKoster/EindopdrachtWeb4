using RestaurantOpdracht_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_BL.Interfaces {
    public interface IKlantRepository {
        bool HeeftKlant(string naam, Contactgegevens contactgegevens);
        bool HeeftKlant(Klant klant);
        Klant RegistreerKlant(Klant klant);
        void UpdateKlant(Klant klant);
        void VerwijderKlant(Klant klant);
    }
}
