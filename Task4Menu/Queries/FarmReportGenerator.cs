using System;
using System.Collections.Generic;
using System.Linq;
using Task4Menu.Models;  // This line is essential for recognizing Cow and Sheep classes


namespace Task4Menu.Queries
{
    internal class FarmReportGenerator
    {
        private readonly List<Cow> cows;
        private readonly List<Sheep> sheep;
        private const decimal CowMilkPrice = 9.4m;
        private const decimal SheepWoolPrice = 6.2m;
        private const decimal GovTaxPerKg = 0.02m;

        public FarmReportGenerator(List<Cow> cows, List<Sheep> sheep)
        {
            this.cows = cows;
            this.sheep = sheep;
        }

        // Display all livestock
        public void DisplayLivestock()
        {
            Console.WriteLine("==Livestock List==");
            Console.WriteLine("ID Cost Weight Colour Milk");
            foreach (var cow in cows)
            {
                Console.WriteLine($"Cow\t{cow.Id}\t{cow.Cost}\t{cow.Weight}\t{cow.Colour}\t{cow.Milk}");
            }
            foreach (var sheep in sheep)
            {
                Console.WriteLine($"Sheep\t{sheep.Id}\t{sheep.Cost}\t{sheep.Weight}\t{sheep.Colour}\t{sheep.Wool}");
            }
        }

        // Calculate and display farm statistics
        public void DisplayFarmStatistics()
        {
            // Daily tax calculation based on total weight
            var totalDailyTax = (cows.Sum(c => c.Weight) + sheep.Sum(s => s.Weight)) * GovTaxPerKg;

            // Monthly tax based on the daily tax
            var monthlyTax = totalDailyTax * 30; // Calculate the tax for 30 days

            // Calculating incomes
            var cowIncome = cows.Sum(c => c.Milk * CowMilkPrice);
            var sheepIncome = sheep.Sum(s => s.Wool * SheepWoolPrice);

            // Total cost from all animals
            var totalCost = cows.Sum(c => c.Cost) + sheep.Sum(s => s.Cost);

            // Total profit after deducting total costs and monthly tax
            var totalProfit = (cowIncome + sheepIncome) - (totalCost + monthlyTax);

            // Average weight calculation
            var averageWeight = (cows.Sum(c => c.Weight) + sheep.Sum(s => s.Weight)) / (cows.Count + sheep.Count);

            Console.WriteLine("\n30 days govt tax: ${0:N2}", monthlyTax);
            Console.WriteLine("Farm daily profit: ${0:N2}", totalProfit);
            Console.WriteLine("Average weight of all livestock: {0:N2} kg", averageWeight);
        }

        public void DisplayProfitStatistics()
        {
            var cowDailyProfit = cows.Select(c => (c.Milk * CowMilkPrice) - c.Cost - (c.Weight * GovTaxPerKg)).Average();
            var sheepDailyProfit = sheep.Select(s => (s.Wool * SheepWoolPrice) - s.Cost - (s.Weight * GovTaxPerKg)).Average();

            var totalDailyProfitCows = cows.Sum(c => (c.Milk * CowMilkPrice) - c.Cost - (c.Weight * GovTaxPerKg));
            var totalDailyProfitSheep = sheep.Sum(s => (s.Wool * SheepWoolPrice) - s.Cost - (s.Weight * GovTaxPerKg));

            Console.WriteLine("Based on current livestock data:");
            Console.WriteLine("On average, a single cow makes a daily profit of: ${0:N2}", cowDailyProfit);
            Console.WriteLine("On average, a single sheep makes a daily profit of: ${0:N2}", sheepDailyProfit);
            Console.WriteLine("Total daily profit of all sheep is: ${0:N2}", totalDailyProfitSheep);
            Console.WriteLine("Total daily profit of all cows is: ${0:N2}", totalDailyProfitCows);
        }

        public void DisplayInvestmentForecast()
        {
            Console.WriteLine("\n==Estimate Future Investment==");
            Console.Write("Enter livestock type (Cow/Sheep): ");
            string livestockType = Console.ReadLine().Trim().ToLower();
            Console.Write("Enter investment quantity of " + (livestockType.Equals("cow") ? "Cows" : "Sheep") + ": ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
            {
                Console.WriteLine("Invalid input for quantity. Please enter a positive integer.");
                return;
            }

            decimal dailyProfit = livestockType == "cow" ?
                CalculateAverageDailyProfit(cows, true) :
                CalculateAverageDailyProfit(sheep, false);
            Console.WriteLine("Buying {0} {1} would bring in an estimated daily profit of: ${2:N2}", quantity, livestockType, dailyProfit * quantity);
        }

        // Helper method to calculate average daily profit for cows or sheep
        private decimal CalculateAverageDailyProfit<T>(List<T> livestock, bool isCow)
        {
            if (isCow)
            {
                return cows.Select(c => (c.Milk * CowMilkPrice) - c.Cost - (c.Weight * GovTaxPerKg)).Average();
            }
            else
            {
                return sheep.Select(s => (s.Wool * SheepWoolPrice) - s.Cost - (s.Weight * GovTaxPerKg)).Average();
            }
        }
        private IEnumerable<Cow> FilterLivestock(IEnumerable<Cow> livestock, string colour)
        {
            return string.IsNullOrEmpty(colour) ? livestock : livestock.Where(l => l.Colour.Equals(colour, StringComparison.OrdinalIgnoreCase));
        }

        // Method to filter sheep based on colour, if necessary
        private IEnumerable<Sheep> FilterLivestock(IEnumerable<Sheep> livestock, string colour)
        {
            return string.IsNullOrEmpty(colour) ? livestock : livestock.Where(l => l.Colour.Equals(colour, StringComparison.OrdinalIgnoreCase));
        }

        public void QueryLivestock()
        {
            Console.WriteLine("\nEnter livestock type (Cow/Sheep): ");
            string type = Console.ReadLine().Trim().ToLower();

            if (type != "cow" && type != "sheep")
            {
                Console.WriteLine("Invalid input");
                return;
            }

            Console.WriteLine("Enter livestock colour (All/Black/Red/White): ");
            string colourInput = Console.ReadLine().Trim().ToLower();
            string colour = colourInput.Equals("all", StringComparison.OrdinalIgnoreCase) ? "" : colourInput;

            if (!new[] { "all", "black", "red", "white" }.Contains(colourInput))
            {
                Console.WriteLine("Invalid input");
                return;
            }

            if (type == "cow")
            {
                var filteredCows = FilterLivestock(cows, colour);
                DisplayFilteredLivestockStats(filteredCows, type, colour);
            }
            else if (type == "sheep")
            {
                var filteredSheep = FilterLivestock(sheep, colour);
                DisplayFilteredLivestockStats(filteredSheep.Cast<Cow>(), type, colour); // Cast or convert as needed
            }
        }
        private void DisplayFilteredLivestockStats(IEnumerable<Cow> livestock, string type, string colour)
        {
            int count = livestock.Count();
            double percentage = (double)count * 100 / (type == "cow" ? cows.Count : sheep.Count);
            decimal dailyTax = livestock.Sum(l => l.Weight) * GovTaxPerKg;
            decimal totalProduce = type == "cow" ? livestock.Sum(c => c.Milk) : livestock.Sum(s => ((Sheep)(object)s).Wool);
            decimal totalProfit = (totalProduce * (type == "cow" ? CowMilkPrice : SheepWoolPrice)) - dailyTax;
            decimal averageWeight = livestock.Average(l => l.Weight);

            Console.WriteLine($"Number of livestocks ({type} in {colour} colour): {count}");
            Console.WriteLine($"Percentage of selected livestock: {percentage:F2}%");
            Console.WriteLine($"Daily tax of selected livestocks: {dailyTax:C2}");
            Console.WriteLine($"Profit: {totalProfit:C2}");
            Console.WriteLine($"Average weight: {averageWeight:F1}kg");
            Console.WriteLine($"Produce amount: {totalProduce:F1}kg");
        }
    }
}