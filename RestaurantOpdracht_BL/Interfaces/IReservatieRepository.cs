using RestaurantOpdracht_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_BL.Interfaces {
    public interface IReservatieRepository {
        void AnnuleerReservatie(int reservatieID);
        List<Reservatie> GeefReservaties(DateTime begin, DateTime eind);
        List<Reservatie> GeefReservatiesInRestaurant(int restaurantID, DateTime begin, DateTime eind);
        List<Reservatie> GeefReservatiesInRestaurant(DateTime datum);
        bool IsTafelGereserveerd(Restaurant restaurant, DateTime datum, int tafelNr);
        void MaakReservatie(Reservatie reservatie);
        bool ReservatieInToekomst(int reservatieID);
        void UpdateReservatie(DateTime datum, int aantalPlaatsen, int reservatieID);
    }
}
