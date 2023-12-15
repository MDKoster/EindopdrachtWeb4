using Microsoft.AspNetCore.Mvc;
using REST_API.Model.Output;
using REST_API.Mappers;
using RestaurantOpdracht_BL.Exceptions;
using RestaurantOpdracht_BL.Managers;
using RestaurantOpdracht_BL.Model;
using REST_API.Model.Input;

namespace REST_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BeheerderController : ControllerBase {
        private RestaurantManager restaurantManager;
        private ReservatieManager reservatieManager;
        private ILogger logger;

        public BeheerderController(RestaurantManager restaurantManager, ReservatieManager reservatieManager, ILoggerFactory loggerFactory) {
            this.restaurantManager = restaurantManager;
            this.reservatieManager = reservatieManager;
            logger = loggerFactory.AddFile("BeheerderLogs.txt").CreateLogger("Beheerder");
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantOutputDTO> Get(int id) {
            try {
                logger.LogTrace($"Restaurant ID: {id} - GET called");
                return Ok(MapFromDomain.MapRestaurantToRestaurantOutput(restaurantManager.GeefRestaurant(id)));
            } catch (ManagerException ex) {
                logger.LogError($"RestaurantID: {id} - GET error - NotFound", ex);
                return NotFound();
            } catch (Exception ex) {
                logger.LogError($"Restaurant ID: {id} - GET error - BadRequest", ex);
                return BadRequest();
            }
        }

        [HttpGet("{id}/{datum}")]
        public ActionResult<List<ReservatieOutputDTO>> Get(int id, DateTime datum) {
            try {
                logger.LogTrace($"Reservaties in restaurant ID: {id} op datum {datum} - GET called");
                return Ok(reservatieManager.GeefReservatiesInRestaurant(id, datum).Select(r => MapFromDomain.MapReservatieToReservatieOutput(r)));
            } catch (ManagerException ex) {
                logger.LogError($"Reservaties in restaurant ID: {id} op datum {datum} - GET error - NotFound", ex);
                return NotFound();
            } catch (Exception ex) {
                logger.LogError($"Reservaties in restaurant ID: {id} op datum: {datum} - GET error - Badrequest", ex);
                return BadRequest();
            }
        }

        [HttpGet("{id}/{start}/{eind}")]
        public ActionResult<List<ReservatieOutputDTO>> Get(int id, DateTime start, DateTime eind) {
            try {
                logger.LogTrace($"Reservaties in restaurant ID: {id} in periode {start} - {eind} - GET called");
                return Ok(reservatieManager.GeefReservatiesInRestaurant(id, start, eind).Select(r => MapFromDomain.MapReservatieToReservatieOutput(r)));
            } catch (ManagerException ex) {
                logger.LogError($"Reservaties in restaurant ID: {id} in periode {start} - {eind} - GET error - NotFound", ex);
                return NotFound();
            } catch (Exception ex) {
                logger.LogError($"Reservaties in restaurant ID: {id} in periode {start} - {eind} - GET error - Badrequest", ex);
                return BadRequest();
            }
        }

        //TODO: betere error return toevoegen mbt internal server error? afhankelijk van waar de error wordt gethrowd?
        [HttpPost]
        public ActionResult<RestaurantOutputDTO> MaakRestaurant([FromBody] RestaurantInputDTO restaurant) {
            try {
                logger.LogTrace($"Maak restaurant aan - {restaurant.Naam} - POST called");
                Contactgegevens contactgegevens = new(restaurant.Tel, restaurant.Email, restaurant.Postcode, restaurant.Gemeentenaam, restaurant.Straatnaam, restaurant.HuisNr);
                RestaurantOutputDTO resto = MapFromDomain.MapRestaurantToRestaurantOutput(restaurantManager.VoegRestaurantToe(restaurant.Naam, restaurant.Keuken, contactgegevens, restaurant.Tafels.Select(t => MapToDomain.MapTafelInputToTafel(t)).ToList()));
                return CreatedAtAction(nameof(Get), new { id = resto.ID }, resto);
            } catch (Exception ex) {
                logger.LogError($"Maak restaurant aan - BadRequest", ex);
                return BadRequest();
            }
        }

        [HttpPut]
        public ActionResult<RestaurantOutputDTO> UpdateRestaurant(int id, [FromBody] RestaurantInputDTO restaurant) {
            try {
                logger.LogTrace($"Update restaurant ID: {id} - PUT called");
                RestaurantOutputDTO updatedRestaurant = MapFromDomain.MapRestaurantToRestaurantOutput(restaurantManager.UpdateRestaurant(MapToDomain.MapRestaurantInputToRestaurant(restaurant)));
                return Ok(updatedRestaurant);
            } catch (ManagerException ex) {
                logger.LogError($"Update restaurant ID: {id} - NotFound", ex);
                return NotFound();
            } catch (Exception ex) {
                logger.LogError($"Update restaurant ID: {id} - BadRequest", ex);
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult VerwijderRestaurant(int id) {
            try {
                logger.LogTrace($"Verwijder restaurant ID: {id} - DELETE called");
                restaurantManager.VerwijderRestaurant(id);
                return NoContent();
            } catch (ManagerException ex) {
                logger.LogError($"Verwijder restaurant ID: {id} - NotFound", ex);
                return NotFound();
            } catch (Exception ex) {
                logger.LogError($"Verwijder restaurant ID: {id} - BadRequest", ex);
                return BadRequest();
            }
        }
    }
}
