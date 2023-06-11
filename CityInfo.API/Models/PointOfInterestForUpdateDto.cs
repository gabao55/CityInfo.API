using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfInterestForUpdateDto
    {
        [MaxLength(50)]
        [MinLength(1)]
        public string? Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
