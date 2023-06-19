using CityInfo.API.Models;

namespace CityInfo.API.DbContext
{
    public interface ICitiesDataStore
    {
        List<CityDto> Cities { get; set; }
    }
}