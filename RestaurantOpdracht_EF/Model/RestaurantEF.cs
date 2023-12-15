using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOpdracht_EF.Model {
    public class RestaurantEF {
        public RestaurantEF() {
        }

        public RestaurantEF(string naam, string keuken, int tel, string email, int postcode, string gemeentenaam, string? straatnaam, string? huisNr, List<TafelEF> tafels, bool status) {
            Naam = naam;
            Keuken = keuken;
            Tel = tel;
            Email = email;
            Postcode = postcode;
            Gemeentenaam = gemeentenaam;
            Straatnaam = straatnaam;
            HuisNr = huisNr;
            Tafels = tafels;
            Status = status;
        }

        public RestaurantEF(int iD, string naam, string keuken, int tel, string email, int postcode, string gemeentenaam, string? straatnaam, string? huisNr, List<TafelEF> tafels, bool status)
            : this (naam, keuken, tel, email, postcode, gemeentenaam, straatnaam, huisNr, tafels, status){
            ID = iD;
        }

        public int ID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Naam { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Keuken { get; set; }
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
        public bool Status { get; set; }
        public List<TafelEF> Tafels { get; set; } = new();
        public List<ReservatieEF> Reservaties { get; set; } = new();
    }
}
