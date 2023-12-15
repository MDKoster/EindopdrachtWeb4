using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_EF.Model {
    public class KlantEF {
        public KlantEF() {
        }

        public KlantEF(string naam, int tel, string email, int postcode, string gemeentenaam, string? straatnaam, string? huisNr) {
            Naam = naam;
            Tel = tel;
            Email = email;
            Postcode = postcode;
            Gemeentenaam = gemeentenaam;
            Straatnaam = straatnaam;
            HuisNr = huisNr;
        }

        public KlantEF(int iD, string naam, int tel, string email, int postcode, string gemeentenaam, string? straatnaam, string? huisNr) : this(naam, tel, email, postcode, gemeentenaam, straatnaam, huisNr) {
            ID = iD;
        }

        public int ID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Naam { get; set; }
        public int Tel { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string Email { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(4)]
        public int Postcode { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Gemeentenaam { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? Straatnaam { get; set; }
        [Column(TypeName = "nvarchar(25)")]
        public string? HuisNr { get; set; }
        public List<ReservatieEF> Reservaties { get; set; }
    }
}
