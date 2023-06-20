using CityInfo.API.DbContexts;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CityInfo.API.Controllers
{

    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesDataStore _citiesDataStore;

        public CitiesController(ICitiesDataStore citiesDataStore)
        {
            _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(_citiesDataStore.Cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity([FromRoute] int id)
        {
            CityDto? city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);

            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
    }
}
