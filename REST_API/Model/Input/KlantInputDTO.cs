namespace REST_API.Model.Input {
    public class KlantInputDTO {
        public KlantInputDTO(string naam, int tel, string email, int postcode, string gemeentenaam, string? straatnaam, string? huisNr) {
            Naam = naam;
            Tel = tel;
            Email = email;
            Postcode = postcode;
            Gemeentenaam = gemeentenaam;
            Straatnaam = straatnaam;
            HuisNr = huisNr;
        }

        public string Naam { get; set; }
        public int Tel { get; set; }
        public string Email { get; set; }
        public int Postcode { get; set; }
        public string Gemeentenaam { get; set; }
        public string? Straatnaam { get; set; }
        public string? HuisNr { get; set; }
    }
}
