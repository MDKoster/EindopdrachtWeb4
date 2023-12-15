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
        private IRestaurantRepository restaurantRepo;

        public ReservatieManager(IReservatieRepository repo, IRestaurantRepository restaurantRepo) {
            this.repo = repo;
            this.restaurantRepo = restaurantRepo;
        }

        public void MaakReservatie(Klant klant, Restaurant restaurant, int aantalPlaatsen, DateTime datum, int tafelNr) {
            if (repo.IsTafelGereserveerd(restaurant, datum, tafelNr)) throw new ManagerException("Tafel is al gereserveerd");
            if (datum.Minute != 0 && datum.Minute != 30) throw new ManagerException("Reserveren kan enkel op xx:00 of xx:30");
            if (restaurant.ID == 0) throw new ManagerException("Restaurant staat nog niet in DB");
            if (klant.ID == 0) throw new ManagerException("Klant staat nog niet in DB");
            //Reservatie object aanmaken omdat alle checks daar uitgevoerd worden
            Reservatie reservatie = new(klant, restaurant, aantalPlaatsen, datum, tafelNr);
            repo.MaakReservatie(reservatie);
        }

        public Reservatie UpdateReservatie(Reservatie reservatie) {
            //TODO: controleren of de nieuwe datum + aantalplaatsen vrij is in het restaurant
            if (!repo.HeeftReservatie(reservatie.ID)) throw new ManagerException("Reservatie bestaat niet");
            if (!repo.IsTafelGereserveerd(reservatie.Restaurant, reservatie.Datum, reservatie.TafelNr)) throw new ManagerException("Reservatie onmogelijk - geen vrije tafel");
            if (reservatie.Datum.Date.CompareTo(DateTime.Now.Date) < 0 &&
                reservatie.Datum.Hour.CompareTo(DateTime.Now.Hour) < 0 &&
                reservatie.Datum.Minute.CompareTo(DateTime.Now.Minute) < 0) throw new ManagerException("Datum reservatie ligt in het verleden");
            if (reservatie.AantalPlaatsen <= 0) throw new ManagerException("Aantal plaatsen moet groter dan 0 zijn.");
            return repo.UpdateReservatie(reservatie);
        }

        public void AnnuleerReservatie(int reservatieID) {
            if (!repo.HeeftReservatie(reservatieID)) throw new ManagerException("Reservatie bestaat niet");
            if (!repo.ReservatieInToekomst(reservatieID)) throw new ManagerException("Reservatie ligt in het verleden");
            repo.AnnuleerReservatie(reservatieID);
        }
 
        public List<Reservatie> GeefReservatiesInPeriode(DateTime begin, DateTime eind) {
            return repo.GeefReservatiesInPeriode(begin, eind);
        }

        public List<Reservatie> GeefReservatiesInRestaurant(int id, DateTime begin, DateTime eind) {
            if (!restaurantRepo.HeeftRestaurant(id)) throw new ManagerException("Restaurant bestaat niet");
            return repo.GeefReservatiesInRestaurant(id, begin, eind);
        }

        public List<Reservatie> GeefReservatiesInRestaurant(int id, DateTime datum) {
            if (!restaurantRepo.HeeftRestaurant(id)) throw new ManagerException("Restaurant bestaat niet");
            return repo.GeefReservatiesInRestaurant(id, datum);
        }
    }
}
