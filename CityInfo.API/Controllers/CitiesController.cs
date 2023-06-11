﻿using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    { 
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity([FromRoute] int id)
        {
            CityDto? city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
    }
}