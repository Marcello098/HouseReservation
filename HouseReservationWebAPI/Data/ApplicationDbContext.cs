using HouseReservationWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseReservationWebAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<House> Houses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<House>().HasData(
            new House()
            {
                Id = 1,
                Name = "Luxury House",
                DetailedInfo = "Some detailed info about the house",
                ImageUrl = "",
                ChargeRate = 500,
                Amenity = "",
                CreatedAt = DateTime.Now
            },
        new House()
            {
                Id = 2,
                Name = "Standard House",
                DetailedInfo = "Some detailed info about the house",
                ImageUrl = "",
                ChargeRate = 300,
                Amenity = "",
                CreatedAt = DateTime.Now
            },
            new House()
            {
                Id = 3,
                Name = "Forest House",
                DetailedInfo = "Some detailed info about the house",
                ImageUrl = "",
                ChargeRate = 400,
                Amenity = "",
                CreatedAt = DateTime.Now
            }
        );
    }
}