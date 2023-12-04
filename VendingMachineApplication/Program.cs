using VendingMachineApplication.Models;
using System;
using System.Collections.Generic;
using VendingMachineApplication.Services;
using VendingMachineApplication.Implementation;

class Program
{
    static void Main(string[] args)
    {
        // Welcome message
        Console.WriteLine("Welcome to Oslo Vending Machine!");
        Console.WriteLine("Available Commands:");
        Console.WriteLine("  list - Displays all products with prices and stock");
        Console.WriteLine("  insert [amount] - Adds the specified amount of credit");
        Console.WriteLine("  recall - Returns the inserted money and resets credit");
        Console.WriteLine("  order [productId] - Orders the specified product if enough credit is available");
        Console.WriteLine("  close - Exits the application");

        // Init VendingMachine and StockService
        VendingMachine vendingMachine = new VendingMachine();
        StockService stockService = new StockService(vendingMachine);

        // Insert initial products and stock
        var products = new List<Product>
        {
            new Product(101, "Pepsi", 20),
            new Product(102, "Fanta", 20),
            new Product(103, "Sprite", 20),
            new Product(104, "Water", 15),
            new Product(105, "Coke", 18),
            new Product(106, "Diet Coke", 22),
            new Product(107, "Apple Juice", 25),
            new Product(108, "Cold Coffee", 19),
            new Product(109, "Iced Tea", 21),
            new Product(110, "Energy Drink", 30),
            new Product(111, "Beer", 18),
            new Product(112, "Ale", 20),
            new Product(113, "Green Tea", 23),
            new Product(114, "Toffee", 24),
            new Product(115, "Kitkat", 17)
        };
        foreach (var product in products)
        {
            vendingMachine.AddProduct(product, 10); // Initial stock for each product
        }

        // Logic to handle commands and exit the app
        bool running = true;
        while (running)
        {
            Console.Write("\n> ");
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');
            string command = parts[0].ToLower();

            switch (command)
            {
                case "list":
                    Console.WriteLine(vendingMachine.DisplayAllProducts());
                    break;
                case "insert":
                    if (parts.Length == 2 && int.TryParse(parts[1], out int amount))
                        Console.WriteLine(vendingMachine.InsertMoney(amount));
                    else
                        Console.WriteLine("Invalid amount");
                    break;
                case "recall":
                    Console.WriteLine(vendingMachine.RecallMoney());
                    break;
                case "order":
                    if (parts.Length == 2 && int.TryParse(parts[1], out int productId))
                        Console.WriteLine(vendingMachine.OrderProduct(productId));
                    else
                        Console.WriteLine("Invalid product id");
                    break;
                case "refill": // only for admins
                    if (parts.Length == 3 && int.TryParse(parts[1], out int refillProductId) && int.TryParse(parts[2], out int refillQuantity))
                    {
                        stockService.RefillStock(refillProductId, refillQuantity);
                        Console.WriteLine($"Refilled {refillQuantity} units of product id {refillProductId}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid refill command");
                    }
                    break;
                case "close":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }

        Console.WriteLine("Thank you for using Oslo Vending Machine. Goodbye!");
    }
}