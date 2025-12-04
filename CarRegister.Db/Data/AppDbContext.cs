using Car_Register.Models;
using CarRegister.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRegister.Db.Data
{
    public class AppDbContext : DbContext       //It's main bridge between C# code ant the database. This class connect project with database, add tables to the database; Dbcontext - class from EF Core library which changes class C# to tables in database
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) //constructor
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)  // this method add column (CarType) to table Car and write which type are cars: class:Car - "Gasoline" or class:ElectricCar - "Electric" 
        {
            modelBuilder.Entity<Car>()
                .HasDiscriminator<string>("CarType")
                .HasValue<Car>("Gasoline")
                .HasValue<ElectricCar>("Electric");
        }

        public DbSet<Car> Cars { get; set; }   //create table "cars" 
    }
}
