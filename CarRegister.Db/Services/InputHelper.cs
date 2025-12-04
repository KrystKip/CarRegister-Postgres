using System;

namespace Car_Register.Services
{
    internal class InputHelper
    {
        public int GetValidInt(string message)           // checking if user gave proper int value
        {
            int value;
            bool valid;
            do
            {
                Console.WriteLine(message);
                valid = int.TryParse(Console.ReadLine(), out value);
                if (valid == false)
                {
                    Console.WriteLine("Enter valid number");
                }
            }
            while (!valid);
            return value;
        }

        public string GetValidString(string message)         // checking if user gave proper string value
        {
            string name;
            do
            {
                Console.WriteLine(message);
                name = Console.ReadLine().ToString();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Input cannot be empty");
                }
            }
            while (string.IsNullOrWhiteSpace(name));
            return name.Trim();             // Trim - remove leading/trailing whitespace
        }

        public int GetValidNum(string message)
        {
            int number;
            bool valid;
            do
            {
                Console.WriteLine(message);
                valid = int.TryParse(Console.ReadLine(), out number);
                if (!valid)
                {
                    Console.WriteLine("This isn't number");
                }
                else if (number != 1 && number != 2)
                {
                    Console.WriteLine("Write proper number: 1 or 2");
                }
            }
            while (!valid || (number != 1 && number != 2));
            return number;
        }
    }
}
