using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City>? GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<IEnumerable<PointOfInterest>>? GetPointsOfInterestForCityAsync(int cityId);
        Task<bool> CityExistsAsync(int cityId);
        Task<PointOfInterest> GetPointOfInterestAsync(int cityId,
            int pointOfInterestId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
    }
}
