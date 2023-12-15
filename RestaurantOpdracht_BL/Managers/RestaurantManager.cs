using RestaurantOpdracht_BL.Exceptions;
using RestaurantOpdracht_BL.Interfaces;
using RestaurantOpdracht_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_BL.Managers {
    public class RestaurantManager {
        private IRestaurantRepository repo;

        public RestaurantManager(IRestaurantRepository repo) {
            this.repo = repo;
        }
        
        public Restaurant VoegRestaurantToe(string naam, string keuken, Contactgegevens contactgegevens, List<Tafel> tafels) {
            if (repo.HeeftRestaurant(naam, contactgegevens)) throw new ManagerException("Restaurant bestaat al");
            return repo.VoegRestaurantToe(new Restaurant(naam, keuken, contactgegevens, tafels));
        }

        public Restaurant UpdateRestaurant(Restaurant restaurant) {
            if (!repo.HeeftRestaurant(restaurant.ID)) throw new ManagerException("Restaurant bestaat niet");
            return repo.UpdateRestaurant(restaurant);
        }

        public void VerwijderRestaurant(int id) {
            if (!repo.HeeftRestaurant(id)) throw new ManagerException("Restaurant bestaat niet");
            repo.VerwijderRestaurant(id);
        }

        public List<Restaurant> GeefRestaurants(int? postcode, string? keuken) {
            if (postcode == null && keuken == null) throw new ManagerException("Zoekparameters zijn allebei leeg");
            return repo.GeefRestaurants(postcode, keuken);
        }

        public List<Restaurant> GeefRestaurantsMetVrijeTafels(int aantalPlaatsen, DateTime datum, int? postcode, string? keuken) {
            if (aantalPlaatsen <= 0) throw new ManagerException("Aantalplaatsen moet groter zijn dan 0");
            return repo.GeefRestaurantsMetVrijeTafels(aantalPlaatsen, datum, postcode, keuken);
        }

        public Restaurant GeefRestaurant(int id) {
            if (!repo.HeeftRestaurant(id)) throw new ManagerException("Restaurant niet gevonden");
            if (id <= 0) throw new ManagerException("ID moet positief zijn");
            return repo.GeefRestaurant(id);
        }
    }
}
