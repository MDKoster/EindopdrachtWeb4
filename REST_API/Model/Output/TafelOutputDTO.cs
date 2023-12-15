namespace REST_API.Model.Output {
    public class TafelOutputDTO {
        public TafelOutputDTO(int tafelNr, int aantalPlaatsen) {
            TafelNr = tafelNr;
            AantalPlaatsen = aantalPlaatsen;
        }

        public int TafelNr { get; set; }
        public int AantalPlaatsen { get; set; }
    }
}
