using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST_API.Mappers;
using REST_API.Model.Input;
using REST_API.Model.Output;
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
        public ActionResult<KlantOutputDTO> Get(int id) {
            try {
                logger.LogTrace($"Klant ID: {id} - GET called");
                return Ok(MapFromDomain.MapKlantToKlantOutput(klantManager.GeefKlant(id)));
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
        public ActionResult<KlantOutputDTO> RegistreerKlant([FromBody] KlantInputDTO klant) {
            try {
                logger.LogTrace($"Maak klant aan - {klant.Naam} - POST called");
                Contactgegevens contactgegevens = new(klant.Tel, klant.Email, klant.Postcode, klant.Gemeentenaam, klant.Straatnaam, klant.HuisNr);
                KlantOutputDTO newKlant = MapFromDomain.MapKlantToKlantOutput(klantManager.RegistreerKlant(klant.Naam, contactgegevens));
                return CreatedAtAction(nameof(Get), new { id = newKlant.ID }, newKlant);
            } catch (Exception ex) {
                logger.LogError($"Maak klant aan - BadRequest", ex);
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("Klant/{id}")]
        public ActionResult<KlantOutputDTO> UpdateKlant(int id, [FromBody] KlantInputDTO klant) {
            try {
                logger.LogTrace($"Update klant ID: {id} - PUT called");
                KlantOutputDTO updatedKlant = MapFromDomain.MapKlantToKlantOutput(klantManager.UpdateKlant(MapToDomain.MapKlantInputToKlant(klant)));
                return Ok(updatedKlant);
            } catch (ManagerException ex) {
                logger.LogError($"Update klant ID: {id} - NotFound", ex);
                return NotFound();
            } catch (Exception ex) {
                logger.LogError($"Update klant ID: {id} - BadRequest", ex);
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

        [HttpGet]
        [Route("Restaurant")]
        public ActionResult<List<RestaurantOutputDTO>> GeefRestaurants([FromQuery]int? postcode, [FromQuery]string? keuken) {
            try {
                logger.LogTrace($"Geef restaurants op basis van postcode en/of keuken: {postcode}/{keuken} - GET called");
                return Ok(restaurantManager.GeefRestaurants(postcode, keuken).Select(r => MapFromDomain.MapRestaurantToRestaurantOutput(r)).ToList());
            } catch (Exception ex) {
                logger.LogError($"Geef restaurants op basis van postcode en/of keuken: {postcode}/{keuken} - GET error - BadRequest", ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("Restaurant/{aantalPlaatsen}/{datum}")]
        public ActionResult<List<RestaurantOutputDTO>> GeefRestaurants(int aantalPlaatsen, DateTime datum, [FromQuery]int? postcode, [FromQuery] string? keuken) {
            try {
                logger.LogTrace($"Geef restaurants met vrije tafels voor {aantalPlaatsen}, op {datum}, optioneel {postcode}/{keuken} - GET called");
                return Ok(restaurantManager.GeefRestaurantsMetVrijeTafels(aantalPlaatsen , datum, postcode, keuken).Select(r => MapFromDomain.MapRestaurantToRestaurantOutput(r)).ToList()); 
            } catch (Exception ex) {
                logger.LogError($"Geef restaurants met vrije tafels voor {aantalPlaatsen}, op {datum}, optioneel {postcode}/{keuken} - GET error - BadRequest", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Reservatie")]
        public ActionResult<ReservatieOutputDTO> MaakReservatie([FromBody] ReservatieInputDTO reservatie) {
            try {
                logger.LogTrace($"Maak reservatie - POST called");
                ReservatieOutputDTO res = MapFromDomain.MapReservatieToReservatieOutput(reservatieManager.MaakReservatie(MapToDomain.MapKlantInputToKlant(reservatie.Klant), MapToDomain.MapRestaurantInputToRestaurant(reservatie.Restaurant), reservatie.AantalPlaatsen, reservatie.Datum, reservatie.TafelNr));
                return CreatedAtAction(nameof(Get), new { id = res.ID }, res);
            } catch (Exception ex) {
                logger.LogError($"Maak reservatie - POST error - BadRequest", ex);
                return BadRequest();
            }
        }

        //TODO: verder aanpassen
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
