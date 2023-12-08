using RestaurantOpdracht_BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_BL.Model {
    public class Klant {
        public Klant(string naam, Contactgegevens contactgegevens) {
            SetNaam(naam);
            Contactgegevens = contactgegevens;
        }

        public Klant(int iD, string naam, Contactgegevens contactgegevens) : this(naam, contactgegevens) {
            SetID(iD);
        }

        public int ID { get; private set; }
        public string Naam { get; private set; }
        public Contactgegevens Contactgegevens { get; set; }

        public void SetID(int iD) {
            if (iD <= 0) throw new ModelException("ID is 0 of negatief");
            ID = iD;
        }

        public void SetNaam(string naam) {
            if (string.IsNullOrWhiteSpace(naam)) throw new ModelException("Naam is leeg");
            Naam = naam;
        }
    }
}
