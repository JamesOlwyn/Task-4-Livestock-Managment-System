using System;
using Task4.Queries;
using Task4Menu.Queries;

namespace Task4Menu
{
    class Program
    {
        static void Main(string[] args)
        {
            FarmDbHelper dbHelper = new FarmDbHelper(); // Correct instantiation if needed, or remove if methods are static
            DisplayMainMenu();
        }

        static void DisplayMainMenu()
        {
            var dbHelper = new FarmDbHelper(); // Remove if methods are static
            var cows = FarmDbHelper.GetAllCows();
            var sheep = FarmDbHelper.GetAllSheep();
            var reportGenerator = new FarmReportGenerator(cows, sheep);
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("\nWelcome to the Farm Database Management System");
                Console.WriteLine("1. Display All Livestock");
                Console.WriteLine("2. Display Farm Statistics");
                Console.WriteLine("3. Add New Livestock Record");
                Console.WriteLine("4. Delete Livestock Record");
                Console.WriteLine("5. Update Livestock Record");
                Console.WriteLine("6. Additional Reports");
                Console.WriteLine("7. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        new FarmReportGenerator(FarmDbHelper.GetAllCows(), FarmDbHelper.GetAllSheep()).DisplayLivestock();
                        break;
                    case "2":
                        new FarmReportGenerator(FarmDbHelper.GetAllCows(), FarmDbHelper.GetAllSheep()).DisplayFarmStatistics();
                        break;
                    case "3":
                        MenuActions.InsertRecord(new FarmDbHelper());
                        break;
                    case "4":
                        MenuActions.DeleteRecord(new FarmDbHelper());
                        break;
                    case "5":
                        MenuActions.UpdateRecord(new FarmDbHelper());
                        break;
                    case "6":
                        AdditionalReports();
                        break;
                    case "7":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
                if (running)
                {
                    Console.WriteLine("\nPress Enter to return to the main menu...");
                    Console.ReadLine();
                }
            }
        }

        static void AdditionalReports()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("\nAdditional Reports:");
                Console.WriteLine("1. Display Profit Statistics");
                Console.WriteLine("2. Display Investment Forecast");
                Console.WriteLine("3. Query Livestock");
                Console.WriteLine("4. Return to Main Menu");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        new FarmReportGenerator(FarmDbHelper.GetAllCows(), FarmDbHelper.GetAllSheep()).DisplayProfitStatistics();
                        break;
                    case "2":
                        new FarmReportGenerator(FarmDbHelper.GetAllCows(), FarmDbHelper.GetAllSheep()).DisplayInvestmentForecast();
                        break;
                    case "3":
                        new FarmReportGenerator(FarmDbHelper.GetAllCows(), FarmDbHelper.GetAllSheep()).QueryLivestock();
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
                if (running)
                {
                    Console.WriteLine("\nPress Enter to return to the reports menu...");
                    Console.ReadLine();
                }
            }
        }
    }
}