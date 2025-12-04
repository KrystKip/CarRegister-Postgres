using Car_Register.Models;
using CarRegister.Db.Data;
using CarRegister.Db.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Car_Register.Services
{
    public class FileService
    {
        private readonly AppDbContext _db;

        public FileService(AppDbContext db)
        {
            _db = db;
        }

        private readonly string _path = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "file.csv");
        
        public void SaveCarsToFile() // save list of cars to csv file
        {
            var cars = _db.Cars.OrderBy(car => car.Id).ToList();
            try
            {
                using (StreamWriter sw = new StreamWriter(_path))
                {
                    sw.WriteLine("Id;Marka;Model;Rok;BatteryCapacity");
                    foreach (var car in cars)
                    {
                        if (car is ElectricCar eCar)
                        {
                            sw.WriteLine($"{eCar.Id};{eCar.Brand};{eCar.Model};{eCar.Year};{eCar.BatteryCapacity}");
                        }
                        else
                        {
                            sw.WriteLine($"{car.Id};{car.Brand};{car.Model};{car.Year};{0}");
                        }
                    }
                }
                Console.WriteLine("File was saved successfully");
            }
            #region Exceptions
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("No access to save file in this location");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Folder path not found");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"A file write error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            #endregion
        }

        public void ShowFileContents()  // show list of cars that was saved to csv file and load into list
        {
            var cars = new List<Car>();
            try
            {
                if (!File.Exists(_path))
                {
                    Console.WriteLine("File doesn't exist.");
                    return;
                }

                cars.Clear();

                using (StreamReader sr = new StreamReader(_path))
                {
                    string header = sr.ReadLine(); // skip first line (header)
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split(';');   // divides line into parts (id, brand, model, year, batterycapacity)

                        if (parts.Length == 5)          // protect from malformed lines
                        {
                            if (!int.TryParse(parts[0], out int id))
                            {
                                // log or skip line
                                continue;
                            }
                            string brand = parts[1].Trim();
                            string model = parts[2].Trim();
                            if (!int.TryParse(parts[3], out int year))
                            {
                                // log or skip line
                                continue;
                            }
                            if (!int.TryParse(parts[4], out int battery))
                            {
                                battery = 0; // or continue
                            }

                            if (battery > 0)
                                cars.Add(new ElectricCar(brand, model, year, battery));  // add electric car to new list 
                            else
                                cars.Add(new Car(brand, model, year));
                        }
                    }
                }

                Console.WriteLine("File read successfully!");
            }

            #region Exceptions
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("You don't have permission to read this file.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"I/O error while reading file: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid data format in file. Please check your CSV structure.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            #endregion 

            foreach (var car in cars)
            {
                car.Info();
                Console.WriteLine();
            }
        }
    
        
    }
    
}
