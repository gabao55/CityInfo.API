using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                new City("New York City")
                {
                    Id = 1,
                    Description = "The one with big Park"
                },
                new City("Rio de Janeiro")
                {
                    Id = 2,
                    Description = "Brazilian Barcelona"
                },
                new City("São Paulo")
                {
                    Id = 3,
                    Description = "Worst city ever"
                });
            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                new PointOfInterest("Central Park")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "Big Park"
                },
                new PointOfInterest("Empire State Building")
                {
                    Id = 2,
                    CityId = 1,
                    Description = "Big building"
                },
                new PointOfInterest("Corcovado")
                {
                    Id = 3,
                    CityId = 2,
                    Description = "Big hill"
                },
                new PointOfInterest("Cristo Redentor")
                {
                    Id = 4,
                    CityId = 2,
                    Description = "Big Crist"
                },
                new PointOfInterest("Paulista")
                {
                    Id = 5,
                    CityId = 3,
                    Description = "Big avenue"
                },
                new PointOfInterest("Copam")
                {
                    Id = 6,
                    CityId = 3,
                    Description = "Big building"
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}