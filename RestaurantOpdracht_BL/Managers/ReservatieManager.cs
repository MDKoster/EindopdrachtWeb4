using RestaurantOpdracht_BL.Exceptions;
using RestaurantOpdracht_BL.Interfaces;
using RestaurantOpdracht_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_BL.Managers {
    public class ReservatieManager {
        private IReservatieRepository repo;

        public ReservatieManager(IReservatieRepository repo) {
            this.repo = repo;
        }
        //o reservatie maken
        public void MaakReservatie(Klant klant, Restaurant restaurant, int aantalPlaatsen, DateTime datum, int tafelNr) {
            if (repo.IsTafelGereserveerd(restaurant, datum, tafelNr)) throw new ManagerException("Tafel is al gereserveerd");
            if (restaurant.ID == 0) throw new ManagerException("Restaurant staat nog niet in DB");
            if (klant.ID == 0) throw new ManagerException("Klant staat nog niet in DB");
            //Reservatie object aanmaken omdat alle checks daar uitgevoerd worden
            Reservatie reservatie = new(klant, restaurant, aantalPlaatsen, datum, tafelNr);
            repo.MaakReservatie(reservatie);
        }
        //o reservatie aanpassen
        //	    datum
        //	    uur
        //	    aantal plaatsen
        public void UpdateReservatie(DateTime datum, int aantalPlaatsen, int reservatieID) {
            //TODO: controleren of de nieuwe datum + aantalplaatsen vrij is in het restaurant
            if (datum.Date.CompareTo(DateTime.Now.Date) < 0 &&
                datum.Hour.CompareTo(DateTime.Now.Hour) < 0 &&
                datum.Minute.CompareTo(DateTime.Now.Minute) < 0) throw new ManagerException("Datum reservatie ligt in het verleden");
            if (aantalPlaatsen <= 0) throw new ManagerException("Aantal plaatsen moet groter dan 0 zijn.");
            repo.UpdateReservatie(datum, aantalPlaatsen, reservatieID);
        }
        //o reservatie annuleren
        public void AnnuleerReservatie(int reservatieID) {
            if (!repo.ReservatieInToekomst(reservatieID)) throw new ManagerException("Reservatie ligt in het verleden");
            repo.AnnuleerReservatie(reservatieID);
        }
 
        //o reservatie opzoeken
        //	    begin/einddatum
        public List<Reservatie> GeefReservaties(DateTime begin, DateTime eind) {
            return repo.GeefReservaties(begin, eind);
        }

        //o overzicht reservaties geven van een resto
        //	    voor specifieke dag
        //	    voor periode begin/einddatum
        public List<Reservatie> GeefReservatiesInRestaurant(int restaurantID, DateTime begin, DateTime eind) {
            return repo.GeefReservatiesInRestaurant(restaurantID, begin, eind);
        }

        public List<Reservatie> GeefReservatiesInRestaurant(DateTime datum) {
            return repo.GeefReservatiesInRestaurant(datum);
        }
    }
}
