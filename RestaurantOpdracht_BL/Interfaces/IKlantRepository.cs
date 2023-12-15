using RestaurantOpdracht_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_BL.Interfaces {
    public interface IKlantRepository {
        Klant GeefKlant(int id);
        bool HeeftKlant(string naam, Contactgegevens contactgegevens);
        bool HeeftKlant(int id);
        Klant RegistreerKlant(Klant klant);
        Klant UpdateKlant(Klant klant);
        void VerwijderKlant(int id);
    }
}
