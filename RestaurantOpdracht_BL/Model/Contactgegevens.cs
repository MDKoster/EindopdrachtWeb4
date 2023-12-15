using RestaurantOpdracht_BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RestaurantOpdracht_BL.Model {
    public class Contactgegevens {
        public Contactgegevens(int tel, string email, int postcode, string gemeentenaam, string? straatnaam, string? huisNr) {
            Tel = tel;
            SetEmail(email);
            SetPostcode(postcode);
            SetGemeentenaam(gemeentenaam);
            Straatnaam = straatnaam;
            HuisNr = huisNr;
        }

        public int Tel { get; set; }
        public string Email { get; set; }
        public int Postcode { get; set; }
        public string Gemeentenaam { get; set; }
        public string? Straatnaam { get; set; }
        public string? HuisNr { get; set; }

        public void SetEmail(string email) {
            if (!email.Contains('@')) throw new ModelException("Email is ongeldig formaat");
            Email = email;
        }

        public void SetPostcode(int postcode) {
            if (postcode.ToString().Length != 4) throw new ModelException("Postcode is ongeldig formaat");
            Postcode = postcode;
        }

        public void SetGemeentenaam(string gemeentenaam) {
            if (string.IsNullOrWhiteSpace(gemeentenaam)) throw new ModelException("Gemeentenaam is leeg");
            Gemeentenaam = gemeentenaam;
        }

        public override bool Equals(object? obj) {
            return obj is Contactgegevens contactgegevens &&
                   Email == contactgegevens.Email &&
                   Postcode == contactgegevens.Postcode &&
                   Gemeentenaam == contactgegevens.Gemeentenaam;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Email, Postcode, Gemeentenaam);
        }
    }
}
