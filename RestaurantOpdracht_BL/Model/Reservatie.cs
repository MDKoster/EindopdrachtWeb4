using RestaurantOpdracht_BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_BL.Model {
    public class Reservatie {
        public Reservatie(Klant klant, Restaurant restaurant, int aantalPlaatsen, DateTime datum, int tafelNr) {
            SetKlant(klant);
            SetRestaurant(restaurant);
            SetAantalPlaatsen(aantalPlaatsen);
            SetDatum(datum);
            SetTafelNr(tafelNr);

        }

        public Reservatie(int iD, Klant klant, Restaurant restaurant, int aantalPlaatsen, DateTime datum, int tafelNr) : this(klant, restaurant, aantalPlaatsen, datum, tafelNr) {
            SetID(iD);
        }

        public Reservatie() {
        }

        public int ID { get; private set; }
        public Klant Klant { get; set; }
        public Restaurant Restaurant { get; set; }
        public int AantalPlaatsen { get; set; }
        public DateTime Datum { get; set; }
        public int TafelNr { get; set; }

        public void SetID(int iD) {
            if (iD <= 0) throw new ModelException("ID moet groter dan 0 zijn.");
            ID = iD;
        }

        public void SetKlant(Klant klant) {
            if (klant == null) throw new ModelException("Klant is null");
            Klant = klant;
        }

        public void SetRestaurant(Restaurant restaurant) {
            if (restaurant == null) throw new ModelException("Restaurant is null");
            Restaurant = restaurant;
        }

        public void SetAantalPlaatsen(int aantalPlaatsen) {
            if (aantalPlaatsen <= 0) throw new ModelException("Aantal plaatsen moet groter dan 0 zijn.");
            AantalPlaatsen = aantalPlaatsen;
        }
        public void SetDatum(DateTime datum) {
            if (datum.Date.CompareTo(DateTime.Now.Date) < 0 &&
                datum.Hour.CompareTo(DateTime.Now.Hour) < 0 &&
                datum.Minute.CompareTo(DateTime.Now.Minute) < 0) throw new ModelException("Datum reservatie ligt in het verleden");
            Datum = datum;
        }

        public void SetTafelNr(int tafelNr) {
            if (tafelNr < 0) throw new ModelException("Tafelnummer kan niet negatief zijn.");
            TafelNr = tafelNr;
        }
    }
}
