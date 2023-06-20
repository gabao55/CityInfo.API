using CityInfo.API.Models;

namespace CityInfo.API.DbContexts
{
    public interface ICitiesDataStore
    {
        List<CityDto> Cities { get; set; }
    }
}