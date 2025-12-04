using CarRegister.Db.Models;
using System;

namespace Car_Register.Models
{
    public class ElectricCar : Car
    {
        public ElectricCar() : base() { }  // constructor without parameters - needed by EF Core

        public ElectricCar(string brand, string model, int year, int batteryCapacity) : base(brand, model, year, batteryCapacity) // constructor for my code
        { }

        public override void Info()
        {
            base.Info();
            Console.Write($"Battery: {BatteryCapacity}");
        }
    }
}
