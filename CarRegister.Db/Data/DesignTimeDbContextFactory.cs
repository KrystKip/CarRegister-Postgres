using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CarRegister.Db.Data
{
    #region description of this class 
    /*
     * DesignTimeDbContextFactory is a helper class that creates your AppDbContext ONLY for Entity Framework Core tools, such as:

dotnet ef migrations add ...

Update-Database

Visual Studio migration commands

EF Core Tools run outside your program, so they cannot see how your real program builds the database connection.
This class gives EF Tools a way to create your DbContext without running your program.

Super simple analogy

Think of it like this:

Your normal program has a “main entrance” → Program.cs

EF Core Tools cannot go through that door

They need their own backdoor

DesignTimeDbContextFactory provides that backdoor
     */
    #endregion
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>  // is a helper class that creates AppDbContext ONLY for Entity Framework 
    
    {
        public AppDbContext CreateDbContext(string[] args)   // When EF Core needs a DbContext at design time (not runtime), it will run this method
        {
            // Determine the path to the project folder where appsettings.json lives.
            // Directory.GetCurrentDirectory() when running EF tools should point to the project.
            var basePath = Directory.GetCurrentDirectory();  // finds appsetting.json or appsetting.local.json

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.local.json", optional: true)
                .Build(); //load appsetting.json

            var conn = config.GetConnectionString("DefaultConnection"); //read connection string
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();  //builds DbContext options. This tells EF: “Use PostgreSQL + this connection string.”

            optionsBuilder.UseNpgsql(conn);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
