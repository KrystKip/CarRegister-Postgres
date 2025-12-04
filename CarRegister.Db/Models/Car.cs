using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRegister.Db.Models
{
    public class Car
    {
        public int Id { get; set; }  // generate by DB
        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";
        public int Year { get; set; }
        public int? BatteryCapacity { get; set; }  // null = gasoline car

        // constructor without parameters - needed by EF Core
        public Car() { }

        // constructor for my code
        public Car(string brand, string model, int year, int? batteryCapacity = null) // int? means that this variable can be null. batteryCapacity = null means that i can write batterycapacity if i want but if i dont it means that batterycapacity is null so car is gasoline
        {
            Brand = brand;
            Model = model;
            Year = year;
            BatteryCapacity = batteryCapacity;
        }

        public virtual void Info()
        {
            Console.Write($"Brand: {Brand}, Model: {Model}, Year: {Year} ");
        }
    }
}

