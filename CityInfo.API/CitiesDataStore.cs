using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public CitiesDataStore()
        {
            Cities= new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York",
                    Description = "The one with Liberty State",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "Big park"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Empire State Building",
                            Description = "Big building"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Rio de Janeiro",
                    Description = "Brazilian Barcelona",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "Sagrada Familia",
                            Description = "Big church"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "Gaudi's houses",
                            Description = "Big decorated and architecture fancy houses"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Sao Paulo",
                    Description = "Worst city ever",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "Avenida Paulista",
                            Description = "Big frustrating avenue"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 6,
                            Name = "Catedral da Se",
                            Description = "Big dangerous church (don't go there at night)"
                        }
                    }
                },
            };
        }
    }
}
