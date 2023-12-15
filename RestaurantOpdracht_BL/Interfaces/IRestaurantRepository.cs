using RestaurantOpdracht_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_BL.Interfaces {
    public interface IRestaurantRepository {
        Restaurant GeefRestaurant(int id);
        List<Restaurant> GeefRestaurants(int? postcode, string? keuken);
        List<Restaurant> GeefRestaurantsMetVrijeTafels(int aantalPlaatsen, DateTime datum, int? postcode, string? keuken);
        bool HeeftRestaurant(string naam, Contactgegevens contactgegevens);
        bool HeeftRestaurant(int restaurantID);
        Restaurant UpdateRestaurant(Restaurant restaurant);
        void VerwijderRestaurant(int id);
        Restaurant VoegRestaurantToe(Restaurant restaurant);
    }
}
