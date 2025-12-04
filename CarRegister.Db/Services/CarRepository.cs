using Car_Register.Models;
using CarRegister.Db.Data;
using CarRegister.Db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Car_Register.Services
{
    internal class CarRepository
    {
        private readonly InputHelper _inputHelper; // object that validates input
        private readonly AppDbContext _db;         //database with cars

        public CarRepository(AppDbContext db)
        {
            _db = db;
            _inputHelper = new InputHelper();
        }

        public void StartListOfCars()  // list of cars to make list not empty at the start of the program
        {
            if (_db.Cars.Any())       //checks if database is empty. If yes then add cars
            {
                return;
            }

            #region List cars
            _db.Cars.Add(new Car("Audi", "A4", 2014));
            _db.Cars.Add(new Car("Toyota", "Yaris2", 2012));
            _db.Cars.Add(new Car("Citroen", "C4", 2011));
            _db.Cars.Add(new Car("Citroen", "C4", 2018));
            _db.Cars.Add(new Car("Toyota", "Yaris", 2020));
            _db.Cars.Add(new ElectricCar("Audi", "A6", 2018, 100));
            #endregion

            _db.SaveChanges();
            Console.WriteLine("Sample cars added to database");
        }

        public void AddCar()  // add car to list
        {
            int numExit = 0;
            do
            {
                Console.WriteLine("Choose which car do you want to add. Write number");
                Console.WriteLine("1. Electric Car");
                Console.WriteLine("2. Gasoline Car");

                // checking if user gave proper answer
                bool check;
                int number;
                Console.WriteLine("Write number 1 or 2");
                do
                {
                    check = int.TryParse(Console.ReadLine(), out number);
                    if (!check)
                    {
                        Console.WriteLine("This isn't number");
                    }
                    else if (number != 1 && number != 2)
                    {
                        Console.WriteLine("Choose between 1 (Electric Car) or 2 (Gasoline Car)");
                    }
                }
                while (!check || (number != 1 && number != 2));

                string Brand = _inputHelper.GetValidString("Write Brand: ");
                string Model = _inputHelper.GetValidString("Write Model:");
                int Year = _inputHelper.GetValidInt("Write Year:");

                // adding new car. User writes all variables
                if (number == 2)
                {

                    _db.Cars.Add(new Car(Brand, Model, Year));
                }
                else if (number == 1)
                {
                    int BatteryCapacity = _inputHelper.GetValidInt("Write BatteryCapacity");
                    _db.Cars.Add(new ElectricCar(Brand, Model, Year, BatteryCapacity));
                }

                _db.SaveChanges();
                Console.WriteLine($"Car added successfully");

                numExit = _inputHelper.GetValidNum("Do you want to add more cars? \n Yes = 1 ------  No = 2"); // checking if user wants to add more
            }
            while (numExit != 2);
        }

        public void RemoveCar()  // delete car from list
        {
            // Shows all then you can check which one you want to remove
            var cars = _db.Cars.OrderBy(car => car.Id).ToList();

            if (!cars.Any())
            {
                Console.WriteLine("No cars to remove");
                return;
            }

            for (int i = 0; i < cars.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                cars[i].Info();
                Console.WriteLine($" (Id: {cars[i].Id})");
            }

            // Checking if proper number is given 
            int choice;
            bool validChoice = false;

            do
            {
                choice = _inputHelper.GetValidInt($"Write number (1-{cars.Count}) of car to remove:") - 1;

                if (choice < 0 || choice >= cars.Count)
                {
                    Console.WriteLine("Invalid number, try again.");
                }
                else
                {
                    validChoice = true;
                }

            } while (!validChoice);

            var carToRemove = cars[choice];

            _db.Cars.Remove(carToRemove);
            _db.SaveChanges();

            Console.WriteLine($"Car with Id {carToRemove.Id} removed successfully");
        }

        public void EditCar()
        {
            var cars = _db.Cars.OrderBy(car => car.Id).ToList();

            if (!cars.Any())
            {
                Console.WriteLine("No cars to remove");
                return;
            }

            for (int i = 0; i < cars.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                cars[i].Info();
                Console.WriteLine($" (Id: {cars[i].Id})");
            }

            int index = _inputHelper.GetValidInt("Enter car number to edit:") - 1;
            if (index < 0 || index >= cars.Count) { Console.WriteLine("Invalid number"); return; }

            var car = cars[index];
            string brand = _inputHelper.GetValidString($"Brand ({car.Brand}):");
            string model = _inputHelper.GetValidString($"Model ({car.Model}):");
            int year = _inputHelper.GetValidInt($"Year ({car.Year}):");

            car.Brand = brand;
            car.Model = model;
            car.Year = year;

            if (car is ElectricCar eCar)
            {
                eCar.BatteryCapacity = _inputHelper.GetValidInt($"BatteryCapacity ({eCar.BatteryCapacity}):");
            }

            _db.SaveChanges();
        }

        public void ShowCars()   // shows all cars from the list
        {
            var cars = _db.Cars.AsNoTracking().OrderBy(car => car.Id).ToList();  // AsNoTracking = EF doesn't truck changes (it takes less memory)
            if (cars.Count == 0)
            {
                Console.WriteLine("No cars in the list");
            }
            else
            {
                Console.WriteLine("List of cars:");
                foreach (Car car in cars)
                {
                    car.Info();
                    Console.WriteLine();
                }
            }
        }

        public void SortCarsByBrand()  // sort Cars by Brand and display
        {
            var cars = _db.Cars.OrderBy(car => car.Brand).AsNoTracking().ToList();
            Console.WriteLine("Cars sorted by Brand");

            foreach (Car car in cars)
            {
                car.Info();
                Console.WriteLine();
            }
        }

        public void SortCarsByYear()
        {
            var cars = _db.Cars.OrderBy(car => car.Year).AsNoTracking().ToList();
            Console.WriteLine("Cars sorted by Year");

            foreach (Car car in cars)
            {
                car.Info();
                Console.WriteLine();
            }
        }  // sort cars by year and display 

        public void ClearDB()  //  delete all cars in database
        {
            if (!_db.Cars.Any())
            {
                Console.WriteLine("Table with cars is empty");
                return;
            }
            _db.Cars.ExecuteDelete();
            Console.WriteLine("Table is cleared");

        }
    }

}
