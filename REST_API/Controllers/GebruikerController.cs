using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantOpdracht_BL.Exceptions;
using RestaurantOpdracht_BL.Interfaces;
using RestaurantOpdracht_BL.Managers;
using RestaurantOpdracht_BL.Model;
using System.Resources;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace REST_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikerController : ControllerBase {
        private KlantManager klantManager;
        private ReservatieManager reservatieManager;
        private RestaurantManager restaurantManager;
        private ILogger logger;

        public GebruikerController(IKlantRepository klantRepo, IReservatieRepository reservatieRepo, IRestaurantRepository restaurantRepo, ILoggerFactory loggerFactory) {
            klantManager = new KlantManager(klantRepo);
            reservatieManager = new ReservatieManager(reservatieRepo, restaurantRepo);
            restaurantManager = new RestaurantManager(restaurantRepo);
            logger = loggerFactory.AddFile("Logs/GebruikerLogs.txt").CreateLogger("Gebruiker");
        }

        [HttpGet]
        [Route("Klant/{id}")]
        public ActionResult<Klant> Get(int id) {
            try {
                logger.LogTrace($"Klant ID: {id} - GET called");
                return Ok(klantManager.GeefKlant(id));
            } catch (ManagerException ex) {
                logger.LogError($"Klant ID: {id} - GET error - NotFound", ex);
                return NotFound();
            } catch (Exception ex) {
                logger.LogError($"Klant ID: {id} - GET error - BadRequest", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Klant")]
        public ActionResult<Klant> RegistreerKlant([FromBody] Klant klant) {
            try {
                logger.LogTrace($"Maak klant aan - {klant.Naam} - POST called");
                klantManager.RegistreerKlant(klant.Naam, klant.Contactgegevens);
                return CreatedAtAction(nameof(Get), new { id = klant.ID }, klant);
            } catch (Exception ex) {
                logger.LogError($"Maak klant aan - BadRequest", ex);
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("Klant")]
        public ActionResult<Klant> UpdateKlant([FromBody] Klant klant) {
            try {
                logger.LogTrace($"Update klant ID: {klant.ID} - PUT called");
                Klant updatedKlant = klantManager.UpdateKlant(klant);
                return Ok(updatedKlant);
            } catch (ManagerException ex) {
                logger.LogError($"Update klant ID: {klant.ID} - NotFound", ex);
                return NotFound();
            } catch (Exception ex) {
                logger.LogError($"Update klant ID: {klant.ID} - BadRequest", ex);
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("Klant/{id}")]
        public IActionResult VerwijderKlant(int id) {
            try {
                logger.LogTrace($"Verwijder klant ID: {id} - DELETE called");
                klantManager.DeleteKlant(id);
                return NoContent();
            } catch (ManagerException ex) {
                logger.LogError($"Verwijder klant ID: {id} - NotFound", ex);
                return NotFound();
            } catch (Exception ex) {
                logger.LogError($"Verwijder klant ID: {id} - BadRequest", ex);
                return BadRequest();
            }
        }

        //TODO: queries gebruiken voor optionele parameters
        [HttpGet]
        [Route("Restaurant/{postcode?}/{keuken?}")]
        public ActionResult<List<Restaurant>> GeefRestaurants(int? postcode, string? keuken) {
            try {
                logger.LogTrace($"Geef restaurants op basis van postcode en/of keuken: {postcode}/{keuken} - GET called");
                return Ok(restaurantManager.GeefRestaurants(postcode, keuken));
            } catch (Exception ex) {
                logger.LogError($"Geef restaurants op basis van postcode en/of keuken: {postcode}/{keuken} - GET error - BadRequest", ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("Restaurant/{aantalPlaatsen}/{datum}/{postcode?}/{keuken?}")]
        public ActionResult<List<Restaurant>> GeefRestaurants(int aantalPlaatsen, DateTime datum, int? postcode, string? keuken) {
            try {
                logger.LogTrace($"Geef restaurants met vrije tafels voor {aantalPlaatsen}, op {datum}, optioneel {postcode}/{keuken} - GET called");
                return Ok(restaurantManager.GeefRestaurantsMetVrijeTafels(aantalPlaatsen , datum, postcode, keuken)); 
            } catch (Exception ex) {
                logger.LogError($"Geef restaurants met vrije tafels voor {aantalPlaatsen}, op {datum}, optioneel {postcode}/{keuken} - GET error - BadRequest", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Reservatie")]
        public ActionResult<Reservatie> MaakReservatie([FromBody] Reservatie reservatie) {
            try {
                logger.LogTrace($"Maak reservatie - POST called");
                reservatieManager.MaakReservatie(reservatie.Klant, reservatie.Restaurant, reservatie.AantalPlaatsen, reservatie.Datum, reservatie.TafelNr);
                return CreatedAtAction(nameof(Get), new { id = reservatie.ID }, reservatie);
            } catch (Exception ex) {
                logger.LogError($"Maak reservatie - POST error - BadRequest", ex);
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("Reservatie/Update/{id}")]
        public ActionResult<Reservatie> UpdateReservatie(int id, [FromBody] Reservatie reservatie) {
            try {
                logger.LogTrace($"Update reservatie ID: {reservatie.ID} - PUT called");
                if (reservatie == null ||  reservatie.ID != id) {
                    return BadRequest();
                }
                Reservatie updatedReservatie = reservatieManager.UpdateReservatie(reservatie);
                return Ok(updatedReservatie);
            } catch (ManagerException ex) {
                logger.LogError($"Update reservatie ID: {reservatie.ID} - NotFound", ex);
                return NotFound();
            } catch (Exception ex) {
                logger.LogError($"Update reservatie ID: {reservatie.ID} - BadRequest", ex);
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("Reservatie/Remove/{id}")]
        public IActionResult VerwijderReservatie(int id) {
            try {
                logger.LogTrace($"Verwijder reservatie ID: {id} - DELETE called");
                reservatieManager.AnnuleerReservatie(id);
                return NoContent();
            } catch (ManagerException ex) {
                logger.LogError($"Verwijder reservatie ID: {id} - NotFound", ex);
                return NotFound();
            } catch (Exception ex) {
                logger.LogError($"Verwijder reservatie ID: {id} - BadRequest", ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("Reservatie/{start}/{eind}")]
        public ActionResult<List<Reservatie>> GeefReservatiesInPeriode(DateTime start, DateTime eind) {
            try {
                logger.LogTrace($"Geef reservaties in periode: {start} - {eind} - GET called");
                return Ok(reservatieManager.GeefReservatiesInPeriode(start, eind));
            } catch (Exception ex) {
                logger.LogError($"Geef reservaties in periode: {start} - {eind} - GET error - BadRequest", ex);
                return BadRequest();
            }
        }
    }
}
