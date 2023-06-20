using CityInfo.API.DbContexts;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly ICitiesDataStore _citiesDataStore;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            IMailService mailService,
            ICitiesDataStore cityDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _citiesDataStore = cityDataStore ?? throw new ArgumentNullException(nameof(cityDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest([FromRoute] int cityId)
        {
            try
            {
                CityDto? city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} not found when accessing points of interest.");
                    return NotFound();
                }

                return Ok(city.PointsOfInterest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(
                    $"Exception while getting points of interest for city with id {cityId}",
                    ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest([FromRoute] int cityId, [FromRoute] int pointOfInterestId)
        {
            CityDto? city = _citiesDataStore.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            PointOfInterestDto? pointOfInterest = city.PointsOfInterest
                .FirstOrDefault(p => p.Id == pointOfInterestId);
            
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }

        [HttpPost]
        public IActionResult CreatePointOfInterest(
            int cityId,
            [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            CityDto? city = _citiesDataStore.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            int maxPointOfInterestId = _citiesDataStore.Cities.SelectMany(
                c => c.PointsOfInterest).Max(p => p.Id);

            PointOfInterestDto finalPointOfInterest = new ()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description,
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            return StatusCode((int)System.Net.HttpStatusCode.Created, city);
        }

        [HttpPut("{pointofinterestid}")]
        public IActionResult UpdatePointOfInterest(
            int cityId,
            int pointOfInterestId,
            PointOfInterestForUpdateDto pointOfInterest
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            CityDto? city = _citiesDataStore.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            PointOfInterestDto? currentPointOfInterest = city.PointsOfInterest.FirstOrDefault(
                p => p.Id == pointOfInterestId);

            if (currentPointOfInterest == null)
            {
                return NotFound();
            }

            currentPointOfInterest.Name = pointOfInterest.Name ?? String.Empty;
            currentPointOfInterest.Description = pointOfInterest.Description;

            return NoContent();
        }

        [HttpPatch("{pointofinterestid}")]
        public IActionResult PartiallyUpdatePointOfInterest(
            int cityId,
            int pointOfInterestId,
            JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument
            )
        {
            CityDto? city = _citiesDataStore.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            PointOfInterestDto? currentPointOfInterest = city.PointsOfInterest.FirstOrDefault(
                p => p.Id == pointOfInterestId);

            if (currentPointOfInterest == null)
            {
                return NotFound();
            }

            PointOfInterestForUpdateDto pointOfInterestToPatch = new ()
            {
                Name = currentPointOfInterest.Name,
                Description = currentPointOfInterest.Description,
            };

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            currentPointOfInterest.Name = pointOfInterestToPatch.Name;
            currentPointOfInterest.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{pointofinterestid}")]
        public IActionResult DeletePointOfInterest(
            int cityId,
            int pointOfInterestId
            )
        {
            CityDto? city = _citiesDataStore.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            PointOfInterestDto? currentPointOfInterest = city.PointsOfInterest.FirstOrDefault(
                p => p.Id == pointOfInterestId);

            if (currentPointOfInterest == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(currentPointOfInterest);

            _mailService.Send("Point of interest deleted.",
                $"Point of interest {currentPointOfInterest.Name} with id {currentPointOfInterest.Id} was deleted.");

            return Accepted();
        }
    }
}
