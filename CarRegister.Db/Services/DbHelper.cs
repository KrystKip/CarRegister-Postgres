using Microsoft.EntityFrameworkCore;
using CarRegister.Db.Data;
using System;

public static class DbHelper     // shows how to connect with database 
{
    public static DbContextOptions<AppDbContext> BuildOptions(string connectionString)  // build options for DbContext
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();  // create object which helps construct 
        optionsBuilder.UseNpgsql(connectionString);  // method which indicates provider (motor Database, Npgsql = PostrgeSQL)
        return optionsBuilder.Options;
    }

    public static bool EnsureDatabaseCreatedAndMigrate(DbContextOptions<AppDbContext> options)   // Check if Database is created and migrate 
    {
        using var db = new AppDbContext(options);   // create object which can communicate with Database
        try
        {
            Console.WriteLine("Checking database connection...");
            if (!db.Database.CanConnect())   // method from EF which tests if connection to DB is working 
            {
                Console.WriteLine("WARNING: Cannot connect to database. Check your PostgreSQL settings.");
                return false;
            }

            Console.WriteLine("Connected to database.");

            db.Database.Migrate();   // method from EF - checks if in folder Migrations are sam changes that weren't done
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR during DB initialization: " + ex.Message);
            return false;
        }
    }
}
