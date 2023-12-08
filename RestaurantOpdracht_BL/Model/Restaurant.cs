using RestaurantOpdracht_BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_BL.Model {
    public class Restaurant {
        public Restaurant(string naam, string keuken, Contactgegevens contactgegevens, Dictionary<int, Tafel> tafels) {
            SetNaam(naam);
            Keuken = keuken;
            Contactgegevens = contactgegevens;
            Tafels = tafels;
        }

        public Restaurant(int iD, string naam, string keuken, Contactgegevens contactgegevens, Dictionary<int, Tafel> tafels) : this(naam, keuken, contactgegevens, tafels) {
            SetID(iD);
        }

        public int ID { get; private set; }
        public string Naam { get; private set; }
        public string Keuken { get; set; }
        public Contactgegevens Contactgegevens { get; set; }
        public Dictionary<int, Tafel> Tafels { get; private set; }

        public void SetID(int iD) {
            if (iD <= 0) throw new ModelException("ID is 0 of negatief");
            ID = iD;
        }

        public void SetNaam(string naam) {
            if (string.IsNullOrWhiteSpace(naam)) throw new ModelException("Naam is leeg");
            Naam = naam;
        }

        public void VoegTafelToe(Tafel tafel) {
            if (tafel == null) throw new ModelException("Tafel is null");
            Tafels.Add(tafel.TafelNr,tafel);
        }
    }
}
