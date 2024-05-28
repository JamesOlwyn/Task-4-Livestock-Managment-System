using System;
using Task4Menu.Models;
using Task4Menu.Queries;

namespace Task4.Queries
{
    public static class MenuActions
    {
        public static void InsertRecord(FarmDbHelper dbHelper)
        {
            Console.WriteLine("====Insert record in database====");
            Console.Write("Enter livestock type (cow/sheep): ");
            string type = Console.ReadLine().Trim().ToLower();

            if (type != "cow" && type != "sheep")
            {
                Console.WriteLine("Invalid livestock type");
                return;
            }

            Console.Write("Enter cost: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal cost))
            {
                Console.WriteLine("Invalid input for cost.");
                return;
            }

            Console.Write("Enter weight: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal weight))
            {
                Console.WriteLine("Invalid input for weight.");
                return;
            }

            Console.Write("Enter livestock colour (black/red/white): ");
            string colour = Console.ReadLine().Trim().ToLower();
            if (colour != "black" && colour != "red" && colour != "white")
            {
                Console.WriteLine("Invalid input for colour.");
                return;
            }

            if (type == "cow")
            {
                Console.Write("Enter milk: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal milk))
                {
                    Console.WriteLine("Invalid input for milk.");
                    return;
                }

                FarmDbHelper.InsertCow(cost, weight, colour, milk); // Call the static method on the class
                Console.WriteLine($"Record added: Cow {dbHelper.GetLastInsertedId()} {cost} {weight} {colour} {milk}");
            }
            else
            {
                Console.Write("Enter wool: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal wool))
                {
                    Console.WriteLine("Invalid input for wool.");
                    return;
                }

                FarmDbHelper.InsertSheep(cost, weight, colour, wool); // Call the static method on the class
                Console.WriteLine($"Record added: Sheep {dbHelper.GetLastInsertedId()} {cost} {weight} {colour} {wool}");
            }
        }

        // Method to delete a livestock record from the database
        public static void DeleteRecord(FarmDbHelper dbHelper)
        {
            Console.WriteLine("====Delete database record====");
            Console.Write("Enter livestock type (cow/sheep): ");
            string livestockType = Console.ReadLine().Trim().ToLower();

            Console.Write("Enter livestock id: ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id < 1)
            {
                Console.WriteLine("Invalid input for ID.");
                return;
            }

            // Make sure to use the livestockType variable to determine which record to delete
            FarmDbHelper.DeleteRecord(id, livestockType); // Call the static method on the class
        }

        // Method to update a livestock record in the database
        public static void UpdateRecord(FarmDbHelper dbHelper)
        {
            Console.WriteLine("====Update database record====");
            Console.Write("Enter livestock type (cow/sheep): ");
            string livestockType = Console.ReadLine().Trim().ToLower();

            Console.Write("Enter livestock id: ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id < 1)
            {
                Console.WriteLine("Invalid input for ID.");
                return;
            }

            Console.Write("Enter cost: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal cost))
            {
                Console.WriteLine("Invalid input for cost.");
                return;
            }

            Console.Write("Enter weight: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal weight))
            {
                Console.WriteLine("Invalid input for weight.");
                return;
            }

            Console.Write("Enter livestock colour (black/red/white): ");
            string colour = Console.ReadLine().Trim().ToLower();
            if (colour != "black" && colour != "red" && colour != "white")
            {
                Console.WriteLine("Invalid input for colour.");
                return;
            }

            Console.Write(livestockType == "cow" ? "Enter milk: " : "Enter wool: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal milkOrWool))
            {
                Console.WriteLine($"Invalid input for {(livestockType == "cow" ? "milk" : "wool")}.");
                return;
            }

            FarmDbHelper.UpdateRecord(id, cost, weight, colour, milkOrWool, livestockType); // Use the collected variables
        }
    }
}