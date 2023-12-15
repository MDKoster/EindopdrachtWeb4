using RestaurantOpdracht_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_BL.Interfaces {
    public interface IReservatieRepository {
        void AnnuleerReservatie(int reservatieID);
        List<Reservatie> GeefReservatiesInPeriode(DateTime begin, DateTime eind);
        List<Reservatie> GeefReservatiesInRestaurant(int restaurantID, DateTime begin, DateTime eind);
        List<Reservatie> GeefReservatiesInRestaurant(int id, DateTime datum);
        bool HeeftReservatie(int reservatieID);
        bool IsTafelGereserveerd(Restaurant restaurant, DateTime datum, int tafelNr);
        void MaakReservatie(Reservatie reservatie);
        bool ReservatieInToekomst(int reservatieID);
        Reservatie UpdateReservatie(Reservatie reservatie);
    }
}
