namespace REST_API.Model.Input {
    public class TafelInputDTO {
        public TafelInputDTO(int tafelNr, int aantalPlaatsen) {
            TafelNr = tafelNr;
            AantalPlaatsen = aantalPlaatsen;
        }

        public int TafelNr { get; set; }
        public int AantalPlaatsen { get; set; }
    }
}
