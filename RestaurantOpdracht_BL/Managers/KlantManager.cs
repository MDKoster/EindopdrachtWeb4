using RestaurantOpdracht_BL.Exceptions;
using RestaurantOpdracht_BL.Interfaces;
using RestaurantOpdracht_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_BL.Managers {
    public class KlantManager {
        private IKlantRepository repo;

        public KlantManager(IKlantRepository repo) {
            this.repo = repo;
        }

        public Klant RegistreerKlant(string naam, Contactgegevens contactgegevens) {
            if (repo.HeeftKlant(naam, contactgegevens)) throw new ManagerException("Klant bestaat al");
            return repo.RegistreerKlant(new Klant(naam, contactgegevens));
        }

        public Klant UpdateKlant(Klant klant) {
            if (!repo.HeeftKlant(klant.ID)) throw new ManagerException("Klant bestaat niet");
            return repo.UpdateKlant(klant);
        }

        public void DeleteKlant(int id) {
            if (!repo.HeeftKlant(id)) throw new ManagerException("Klant bestaat niet");
            repo.VerwijderKlant(id);
        }

        public Klant GeefKlant(int id) {
            if (!repo.HeeftKlant(id)) throw new ManagerException("Klant bestaat niet");
            if (id <= 0) throw new ManagerException("ID moet groter zijn dan 0");
            return repo.GeefKlant(id);
        }
    }
}
