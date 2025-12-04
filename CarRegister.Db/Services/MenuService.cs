using Car_Register.Models;
using CarRegister.Db.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Car_Register.Services
{
    internal class MenuService
    {
        private readonly CarRepository _carRepository;
        private readonly FileService _fileService;

        public MenuService(CarRepository carRepository, FileService fileService)
        {
            _carRepository = carRepository;
            _fileService = fileService;
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("==== Car Register Menu ====");
                Console.WriteLine("Choose your option:");
                Console.WriteLine("1. Add car");
                Console.WriteLine("2. Remove Car");
                Console.WriteLine("3. Show all cars");
                Console.WriteLine("4. Save to file");
                Console.WriteLine("5. Read file");
                Console.WriteLine("6. Sort cars by year");
                Console.WriteLine("7. Sort cars by brand");
                Console.WriteLine("8. Edit Car");
                Console.WriteLine("9. Clear Database");
                Console.WriteLine("10. Exit");
                Console.WriteLine("Choose option (1-10)");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _carRepository.AddCar();
                        break;
                    case "2":
                        _carRepository.RemoveCar();
                        break;
                    case "3":
                        _carRepository.ShowCars();
                        break;
                    case "4":
                        _fileService.SaveCarsToFile();
                        break;
                    case "5":
                        _fileService.ShowFileContents();
                        break;
                    case "6":
                        _carRepository.SortCarsByYear();
                        break;
                    case "7":
                        _carRepository.SortCarsByBrand();
                        break;
                    case "8":
                        _carRepository.EditCar();
                        break;
                    case "9":
                        _carRepository.ClearDB();
                        break;
                    case "10":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                }
            }
        }
    }
}
