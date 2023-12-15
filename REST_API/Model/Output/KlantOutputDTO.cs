namespace REST_API.Model.Output {
    public class KlantOutputDTO {
        public KlantOutputDTO(int iD, string naam, int tel, string email, int postcode, string gemeentenaam, string? straatnaam, string? huisNr) {
            ID = iD;
            Naam = naam;
            Tel = tel;
            Email = email;
            Postcode = postcode;
            Gemeentenaam = gemeentenaam;
            Straatnaam = straatnaam;
            HuisNr = huisNr;
        }

        public int ID {  get; set; }
        public string Naam { get; set; }
        public int Tel { get; set; }
        public string Email { get; set; }
        public int Postcode { get; set; }
        public string Gemeentenaam { get; set; }
        public string? Straatnaam { get; set; }
        public string? HuisNr { get; set; }
    }
}
