using Car_Register.Services;
using CarRegister.Db.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;


namespace CarRegister.Db
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
                .Build();  //configurationBuilder loads json file


            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var options = DbHelper.BuildOptions(connectionString);
            var dbOk = DbHelper.EnsureDatabaseCreatedAndMigrate(options);

            if (!dbOk)            // checking if database is connected and then: if yes - do program, if not - close program
            {
                Console.WriteLine("Database is not available. Program will exit.");
                Console.WriteLine("Press any key to close...");
                Console.ReadKey();
                return;   // end main and close program
            }

            using var db = new AppDbContext(options);

            var repo = new CarRepository(db);
            var fileservice = new FileService(db);
            var menuService = new MenuService(repo, fileservice);

            // Add initial sample cars if table is empty
            repo.StartListOfCars();

            // Run interactive menu
            menuService.Run();

            Console.WriteLine("Program finished. Press any key to exit...");
            Console.ReadKey();
        }
    }

}
