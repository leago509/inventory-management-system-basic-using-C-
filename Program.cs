
using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }

    public Product(string name, double price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }
}

class Program
{
    static List<Product> products = new List<Product>();

    static void Main()
    {
        int choice;

        do
        {
            Console.WriteLine("\n=== Inventory Management System ===");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Update Stock");
            Console.WriteLine("3. View Products");
            Console.WriteLine("4. Remove Product");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Please enter a valid number.");
                choice = 0;
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddProduct();
                    break;
                case 2:
                    UpdateStock();
                    break;
                case 3:
                    ViewProducts();
                    break;
                case 4:
                    RemoveProduct();
                    break;
                case 5:
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

        } while (choice != 5);
    }

    static void AddProduct()
    {
        Console.Write("Product name: ");
        string name = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(name) ||
            products.Any(p => p.Name.Equals(
                name, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Invalid or duplicate product name.");
            return;
        }

        Console.Write("Product price: ");
        if (!double.TryParse(Console.ReadLine(), out double price)
            || price < 0)
        {
            Console.WriteLine("Enter a valid price.");
            return;
        }

        Console.Write("Stock quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int stock)
            || stock < 0)
        {
            Console.WriteLine("Enter a valid stock quantity.");
            return;
        }

        products.Add(new Product(name, price, stock));
        Console.WriteLine("Product added successfully!");
    }

    static void UpdateStock()
    {
        Console.Write("Enter product name: ");
        string name = Console.ReadLine() ?? "";

        Product? product = products.FirstOrDefault(
            p => p.Name.Equals(
                name, StringComparison.OrdinalIgnoreCase));

        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        Console.Write("Enter quantity change (+ restock, - sold): ");

        if (!int.TryParse(Console.ReadLine(), out int change))
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        if (product.Stock + change < 0)
        {
            Console.WriteLine("Not enough stock available.");
            return;
        }

        product.Stock += change;
        Console.WriteLine("Stock updated successfully!");
    }

    static void ViewProducts()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        foreach (Product product in products)
        {
            Console.WriteLine(
                $"Name: {product.Name} | " +
                $"Price: {product.Price:F2} | " +
                $"Stock: {product.Stock}");
        }
    }

    static void RemoveProduct()
    {
        Console.Write("Enter product name to remove: ");
        string name = Console.ReadLine() ?? "";

        Product? product = products.FirstOrDefault(
            p => p.Name.Equals(
                name, StringComparison.OrdinalIgnoreCase));

        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        products.Remove(product);
        Console.WriteLine("Product removed successfully!");
    }
}
