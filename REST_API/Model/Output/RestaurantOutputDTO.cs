using REST_API.Model.Input;

namespace REST_API.Model.Output {
    public class RestaurantOutputDTO {
        public RestaurantOutputDTO(int iD, string naam, string keuken, int tel, string email, int postcode, string gemeentenaam, string? straatnaam, string? huisNr, List<TafelOutputDTO> tafels) {
            ID = iD;
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

        public int ID { get; set; }
        public string Naam { get; set; }
        public string Keuken { get; set; }
        public int Tel { get; set; }
        public string Email { get; set; }
        public int Postcode { get; set; }
        public string Gemeentenaam { get; set; }
        public string? Straatnaam { get; set; }
        public string? HuisNr { get; set; }
        public List<TafelOutputDTO> Tafels { get; set; }
    }
}
