using RestaurantOpdracht_BL.Interfaces;
using RestaurantOpdracht_BL.Model;
using RestaurantOpdracht_EF.Exceptions;
using RestaurantOpdracht_EF.Mappers;
using RestaurantOpdracht_EF.Model;

namespace RestaurantOpdracht_EF.Repositories {
    public class KlantRepository : IKlantRepository {

        private RestaurantContext ctx = new RestaurantContext();

        public KlantRepository() {
        }

        private void SaveAndClear() {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public bool HeeftKlant(string naam, Contactgegevens contactgegevens) {
            try {
                return ctx.Klant.Any(
                    kl => kl.Naam == naam && 
                    kl.Email == contactgegevens.Email && 
                    kl.Postcode == contactgegevens.Postcode && 
                    kl.Gemeentenaam == contactgegevens.Gemeentenaam);
            } catch (Exception ex) {
                throw new RepositoryException("HeeftKlant", ex);
            }
        }

        public bool HeeftKlant(int id) {
            try {
                return ctx.Klant.Any(kl => kl.ID == id);
            } catch (Exception ex ) {
                throw new RepositoryException("HeeftKlant", ex);
            }
        }

        public Klant RegistreerKlant(Klant klant) {
            try {
                KlantEF klantEF = MapFromDomain.MapKlantToKlantEF(klant, ctx);
                ctx.Klant.Add(klantEF);
                SaveAndClear();
                return MapToDomain.MapKlantEFToKlant(klantEF);
            } catch (Exception ex) {
                throw new RepositoryException("RegistreerKlant", ex);
            }
        }

        public Klant UpdateKlant(Klant klant) {
            try {
                KlantEF klantEF = MapFromDomain.MapKlantToKlantEF(klant, ctx);
                ctx.Klant.Update(klantEF);
                SaveAndClear();
                return MapToDomain.MapKlantEFToKlant(klantEF);
            } catch (Exception ex) {
                throw new RepositoryException("UpdateKlant", ex);
            }
        }

        public void VerwijderKlant(int id) {
            try {
                KlantEF klantEF = ctx.Klant.Find(id);
                ctx.Klant.Remove(klantEF);
                SaveAndClear();
            } catch (Exception ex) {
                throw new RepositoryException("VerwijderKlant", ex);
            }
        }

        public Klant GeefKlant(int id) {
            try {
                return MapToDomain.MapKlantEFToKlant(ctx.Klant.Find(id));
            } catch (Exception ex) {
                throw new RepositoryException("GeefKlant", ex);
            }
        }
    }
}
