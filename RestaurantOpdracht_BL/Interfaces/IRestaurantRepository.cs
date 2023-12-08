using RestaurantOpdracht_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_BL.Interfaces {
    public interface IRestaurantRepository {
        List<Restaurant> GeefRestaurants(int? postcode, string? keuken);
        List<Restaurant> GeefRestaurantsMetVrijeTafels(int aantalPlaatsen, DateTime datum);
        bool HeeftRestaurant(string naam, Contactgegevens contactgegevens);
        bool HeeftRestaurant(Restaurant restaurant);
        void UpdateRestaurant(Restaurant restaurant);
        void VerwijderRestaurant(Restaurant restaurant);
        Restaurant VoegRestaurantToe(Restaurant restaurant);
    }
}
