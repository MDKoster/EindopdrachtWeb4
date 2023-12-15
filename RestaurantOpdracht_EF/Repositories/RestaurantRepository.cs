using Microsoft.EntityFrameworkCore;
using RestaurantOpdracht_BL.Interfaces;
using RestaurantOpdracht_BL.Model;
using RestaurantOpdracht_EF.Exceptions;
using RestaurantOpdracht_EF.Mappers;
using RestaurantOpdracht_EF.Model;

namespace RestaurantOpdracht_EF.Repositories {
    public class RestaurantRepository : IRestaurantRepository {

        private RestaurantContext ctx;

        public RestaurantRepository(RestaurantContext ctx) {
            this.ctx = ctx;
        }

        private void SaveAndClear() {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public Restaurant GeefRestaurant(int id) {
            try {
                return MapToDomain.MapRestaurantEFToRestaurant(
                    ctx.Restaurant.AsNoTracking()
                    .Include(r => r.Tafels)
                    .Where(r => r.ID == id)
                    .FirstOrDefault());
            } catch (Exception ex) {
                throw new RepositoryException("GeefRestaurant", ex);
            }
        }

        public List<Restaurant> GeefRestaurants(int? postcode, string? keuken) {
            try {
                if (keuken == null) {
                    return ctx.Restaurant.AsNoTracking()
                        .Include(r => r.Tafels)
                        .Include(r => r.Reservaties)
                        .Where(r => r.Postcode.Equals(postcode))
                        .Select<RestaurantEF, Restaurant>(r => MapToDomain.MapRestaurantEFToRestaurant(r))
                        .ToList();
                } else if (postcode == null) {
                    return ctx.Restaurant.AsNoTracking()
                        .Include(r => r.Tafels)
                        .Include(r => r.Reservaties)
                        .Where(r => r.Keuken.Equals(keuken))
                        .Select<RestaurantEF, Restaurant>(r => MapToDomain.MapRestaurantEFToRestaurant(r))
                        .ToList();
                } else {
                    return ctx.Restaurant.AsNoTracking()
                        .Include(r => r.Tafels)
                        .Include(r => r.Reservaties)
                        .Where(r => r.Keuken.Equals(keuken))
                        .Where(r => r.Postcode.Equals(postcode))
                        .Select<RestaurantEF, Restaurant>(r => MapToDomain.MapRestaurantEFToRestaurant(r))
                        .ToList();
                }
            } catch (Exception ex) {
                throw new RepositoryException("GeefRestaurants", ex);
            }
        }

        public List<Restaurant> GeefRestaurantsMetVrijeTafels(int aantalPlaatsen, DateTime datum, int? postcode, string? keuken) {
            try {
                List<Restaurant> restaurants = new List<Restaurant>();
                if (postcode == null && keuken == null) {
                    restaurants = ctx.Restaurant.AsNoTracking()
                    .Include(r => r.Tafels)
                    .Include(r => r.Reservaties)
                    .Where(r => r.Tafels.Any(t => t.AantalPlaatsen >= aantalPlaatsen &&
                    !r.Reservaties.Any(res => res.TafelNr == t.TafelNr && res.Datum == datum)))
                    .Select<RestaurantEF, Restaurant>(r => MapToDomain.MapRestaurantEFToRestaurant(r))
                    .ToList();
                } else if (postcode == null && keuken != null) {
                    restaurants = ctx.Restaurant.AsNoTracking()
                    .Include(r => r.Tafels)
                    .Include(r => r.Reservaties)
                    .Where(r => r.Keuken.Contains(keuken))
                    .Where(r => r.Tafels.Any(t => t.AantalPlaatsen >= aantalPlaatsen &&
                    !r.Reservaties.Any(res => res.TafelNr == t.TafelNr && res.Datum == datum)))
                    .Select<RestaurantEF, Restaurant>(r => MapToDomain.MapRestaurantEFToRestaurant(r))
                    .ToList();
                } else if (postcode != null && keuken == null) {
                    restaurants = ctx.Restaurant.AsNoTracking()
                    .Include(r => r.Tafels)
                    .Include(r => r.Reservaties)
                    .Where(r => r.Postcode.Equals(postcode))
                    .Where(r => r.Tafels.Any(t => t.AantalPlaatsen >= aantalPlaatsen &&
                    !r.Reservaties.Any(res => res.TafelNr == t.TafelNr && res.Datum == datum)))
                    .Select<RestaurantEF, Restaurant>(r => MapToDomain.MapRestaurantEFToRestaurant(r))
                    .ToList();
                } else if (postcode != null && keuken != null) {
                    restaurants = ctx.Restaurant.AsNoTracking()
                    .Include(r => r.Tafels)
                    .Include(r => r.Reservaties)
                    .Where(r => r.Keuken.Contains(keuken))
                    .Where(r => r.Postcode.Equals(postcode))
                    .Where(r => r.Tafels.Any(t => t.AantalPlaatsen >= aantalPlaatsen &&
                    !r.Reservaties.Any(res => res.TafelNr == t.TafelNr && res.Datum == datum)))
                    .Select<RestaurantEF, Restaurant>(r => MapToDomain.MapRestaurantEFToRestaurant(r))
                    .ToList();
                }
                return restaurants;
            } catch (Exception ex) {
                throw new RepositoryException("GeefRestaurantsMetVrijeTafels", ex);
            }
        }

        public bool HeeftRestaurant(string naam, Contactgegevens contactgegevens) {
            try {
                return ctx.Restaurant.Any(
                    res => res.Naam == naam &&
                    res.Email == contactgegevens.Email &&
                    res.Postcode == contactgegevens.Postcode &&
                    res.Gemeentenaam == contactgegevens.Gemeentenaam);
            } catch (Exception ex) {
                throw new RepositoryException("HeeftRestaurant", ex);
            }
        }

        public bool HeeftRestaurant(int restaurantID) {
            try {
                return ctx.Restaurant.Any(res => res.ID == restaurantID);
            } catch (Exception ex) {
                throw new RepositoryException("HeeftRestaurant", ex);
            }
        }

        public Restaurant UpdateRestaurant(Restaurant restaurant) {
            try {
                ctx.Restaurant.Update(MapFromDomain.MapRestaurantToRestaurantEF(restaurant));
                SaveAndClear();
                return MapToDomain.MapRestaurantEFToRestaurant(ctx.Restaurant.Find(restaurant.ID));
            } catch (Exception ex) {
                throw new RepositoryException("UpdateRestaurant", ex);
            }
        }

        public void VerwijderRestaurant(int id) {
            try {
                RestaurantEF restaurantEF = ctx.Restaurant.Find(id);
                restaurantEF.Status = false;
                ctx.Restaurant.Update(restaurantEF);
                SaveAndClear();
            } catch (Exception ex) {
                throw new RepositoryException("VerwijderRestaurant", ex);
            }
        }

        public Restaurant VoegRestaurantToe(Restaurant restaurant) {
            try {
                RestaurantEF restaurantEF = MapFromDomain.MapRestaurantToRestaurantEF(restaurant);
                ctx.Restaurant.Add(restaurantEF);
                SaveAndClear();
                return MapToDomain.MapRestaurantEFToRestaurant(restaurantEF);
            } catch (Exception ex) {
                throw new RepositoryException("VoegRestaurantToe", ex);
            }
        }
    }
}
