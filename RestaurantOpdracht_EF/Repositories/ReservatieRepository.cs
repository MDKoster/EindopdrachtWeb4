using Microsoft.EntityFrameworkCore;
using RestaurantOpdracht_BL.Interfaces;
using RestaurantOpdracht_BL.Model;
using RestaurantOpdracht_EF.Exceptions;
using RestaurantOpdracht_EF.Mappers;
using RestaurantOpdracht_EF.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestaurantOpdracht_EF.Repositories {
    public class ReservatieRepository : IReservatieRepository {

        private RestaurantContext ctx = new RestaurantContext();

        public ReservatieRepository() {
        }

        private void SaveAndClear() {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public void AnnuleerReservatie(int reservatieID) {
            try {
                ctx.Reservatie.Remove(ctx.Reservatie.Find(reservatieID));
                SaveAndClear();
            } catch (Exception ex) {
                throw new RepositoryException("AnnuleerReservatie", ex);
            }
        }

        public List<Reservatie> GeefReservatiesInPeriode(DateTime begin, DateTime eind) {
            try {
                return ctx.Reservatie.AsNoTracking()
                    .Include(r => r.Restaurant)
                    .Include(r => r.Klant)
                    .Where(r => r.Datum.CompareTo(begin) > 0)
                    .Where(r => r.Datum.CompareTo(eind) < 0)
                    .Select<ReservatieEF, Reservatie>(r => MapToDomain.MapReservatieEFToReservatie(r))
                    .ToList();
            } catch (Exception ex) {
                throw new RepositoryException("GeefReservaties", ex);
            }
        }

        public List<Reservatie> GeefReservatiesInRestaurant(int restaurantID, DateTime begin, DateTime eind) {
            try {
                return ctx.Reservatie.AsNoTracking()
                     .Include(r => r.Restaurant)
                    .Include(r => r.Klant)
                    .Where(r => r.ID == restaurantID)
                    .Where(r => r.Datum.CompareTo(begin) > 0)
                    .Where(r => r.Datum.CompareTo(eind) < 0)
                    .Select<ReservatieEF, Reservatie>(r => MapToDomain.MapReservatieEFToReservatie(r))
                    .ToList();
            } catch (Exception ex) {
                throw new RepositoryException("GeefReservatiesInRestaurant", ex);
            }
        }

        public List<Reservatie> GeefReservatiesInRestaurant(int id, DateTime datum) {
            try {
                return ctx.Reservatie.AsNoTracking()
                    .Include(r => r.Restaurant)
                    .Include(r => r.Klant)
                    .Where(r => r.ID == id)
                    .Where(r => r.Datum.Date == datum.Date)
                    .Select<ReservatieEF, Reservatie>(r => MapToDomain.MapReservatieEFToReservatie(r))
                    .ToList();
            } catch (Exception ex) {
                throw new RepositoryException("GeefReservatiesInRestaurant", ex);
            }
        }

        public List<Reservatie> GeefReservatiesInRestaurant(DateTime datum) {
            try {
                return ctx.Reservatie.AsNoTracking()
                    .Include(r => r.Restaurant)
                    .Include(r => r.Klant)
                    .Where(r => r.Datum.Date == datum.Date)
                    .Select<ReservatieEF, Reservatie>(r => MapToDomain.MapReservatieEFToReservatie(r))
                    .ToList();
            } catch (Exception ex) {
                throw new RepositoryException("GeefReservatiesInRestaurant", ex);
            }
        }

        public bool HeeftReservatie(int reservatieID) {
            try {
                return ctx.Reservatie.Any(r => r.ID == reservatieID);
            } catch (Exception ex) {
                throw new RepositoryException("HeeftReservatie", ex);
            }
        }

        public bool IsTafelGereserveerd(Restaurant restaurant, DateTime datum, int tafelNr) {
            try {
                DateTime endTime = datum.AddHours(1.5);

                return ctx.Reservatie
                    .Any(r => r.Restaurant.ID == restaurant.ID &&
                    r.TafelNr == tafelNr &&
                    r.Datum > datum &&
                    r.Datum < endTime);
            } catch (Exception ex) {
                throw new RepositoryException("IsTafelGereserveerd", ex);
            }
        }

        public Reservatie MaakReservatie(Reservatie reservatie) {
            try {
                ReservatieEF res = MapFromDomain.MapReservatieToReservatieEF(reservatie, ctx);
                ctx.Reservatie.Add(res);
                SaveAndClear();
                return MapToDomain.MapReservatieEFToReservatie(res);
            } catch (Exception ex) {
                throw new RepositoryException("MaakReservatie", ex);
            }
        }

        public bool ReservatieInToekomst(int reservatieID) {
            try {
                return ctx.Reservatie.AsNoTracking().Where(r => r.ID == reservatieID).First().Datum > DateTime.Now;
            } catch (Exception ex) {
                throw new RepositoryException("ReservatieInToekomst", ex);
            }
        }

        public Reservatie UpdateReservatie(Reservatie reservatie) {
            try {
                ReservatieEF reservatieEF = ctx.Reservatie.Find(reservatie.ID);
                reservatieEF.Datum = reservatie.Datum;
                reservatieEF.AantalPlaatsen = reservatie.AantalPlaatsen;
                ctx.Reservatie.Update(reservatieEF);
                SaveAndClear();
                return MapToDomain.MapReservatieEFToReservatie(reservatieEF);
            } catch (Exception ex) {
                throw new RepositoryException("UpdateReservatie", ex);
            }
        }
    }
}
