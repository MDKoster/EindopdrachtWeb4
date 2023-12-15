using RestaurantOpdracht_BL.Model;

namespace REST_API.Model.Input {
    public class RestaurantInputDTO {
        public RestaurantInputDTO(string naam, string keuken, int tel, string email, int postcode, string gemeentenaam, string? straatnaam, string? huisNr, List<TafelInputDTO> tafels) {
            Naam = naam;
            Keuken = keuken;
            Tel = tel;
            Email = email;
            Postcode = postcode;
            Gemeentenaam = gemeentenaam;
            Straatnaam = straatnaam;
            HuisNr = huisNr;
            Tafels = tafels;
        }

        public string Naam { get; set; }
        public string Keuken { get; set; }
        public int Tel { get; set; }
        public string Email { get; set; }
        public int Postcode { get; set; }
        public string Gemeentenaam { get; set; }
        public string? Straatnaam { get; set; }
        public string? HuisNr { get; set; }
        public List<TafelInputDTO> Tafels { get; set; }
    }
}
