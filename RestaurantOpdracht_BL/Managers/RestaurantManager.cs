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
        
        public Restaurant VoegRestaurantToe(string naam, string keuken, Contactgegevens contactgegevens, Dictionary<int, Tafel> tafels) {
            //TODO: resto toevoegen aan DB en ID toevoegen alvorens return
            if (repo.HeeftRestaurant(naam, contactgegevens)) throw new ManagerException("Restaurant bestaat al");
            return repo.VoegRestaurantToe(new Restaurant(naam, keuken, contactgegevens, tafels));
        }

        public void UpdateRestaurant(Restaurant restaurant) {
            if (!repo.HeeftRestaurant(restaurant)) throw new ManagerException("Restaurant bestaat niet");
            repo.UpdateRestaurant(restaurant);
        }

        public void VerwijderRestaurant(Restaurant restaurant) {
            if (!repo.HeeftRestaurant(restaurant)) throw new ManagerException("Restaurant bestaat niet");
            repo.VerwijderRestaurant(restaurant);
        }

        //o resto opzoeken
        //	    op locatie en/of keuken
        public List<Restaurant> GeefRestaurants(int? postcode, string? keuken) {
            if (postcode == null && keuken == null) throw new ManagerException("Zoekparameters zijn allebei leeg");
            return repo.GeefRestaurants(postcode, keuken);
        }

        //o overzicht opvragen van resto's met vrije tafel voor n plaatsen op bepaalde datum
        public List<Restaurant> GeefRestaurantsMetVrijeTafels(int aantalPlaatsen, DateTime datum) {
            return repo.GeefRestaurantsMetVrijeTafels(aantalPlaatsen, datum);
        }
    }
}
