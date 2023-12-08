﻿using RestaurantOpdracht_BL.Exceptions;
using RestaurantOpdracht_BL.Interfaces;
using RestaurantOpdracht_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_BL.Managers {
    public class KlantManager {
        private IKlantRepository repo;

        public KlantManager(IKlantRepository repo) {
            this.repo = repo;
        }

        public Klant RegistreerKlant(string naam, Contactgegevens contactgegevens) {
            //TODO: klant toevoegen aan DB en ID toevoegen alvorens return
            if (repo.HeeftKlant(naam, contactgegevens)) throw new ManagerException("Klant bestaat al");
            return repo.RegistreerKlant(new Klant(naam, contactgegevens));
        }

        public void UpdateKlant(Klant klant) {
            if (!repo.HeeftKlant(klant)) throw new ManagerException("Klant bestaat niet");
            repo.UpdateKlant(klant);
        }

        public void DeleteKlant(Klant klant) {
            if (!repo.HeeftKlant(klant)) throw new ManagerException("Klant bestaat niet");
            repo.VerwijderKlant(klant);
        }
    }
}