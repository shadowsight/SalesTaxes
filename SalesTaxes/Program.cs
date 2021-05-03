using System;
using SalesTaxes.Models;

namespace SalesTaxes
{
    public static class Program
    {
        private static readonly Cart Cart = new Cart();

        public static void Main(string[] args)
        {
            AddItemsToShoppingCart();
            GenerateReceipt();
        }

        private static void AddItemsToShoppingCart()
        {
            Console.WriteLine("Please items to your shopping cart.\n");
            Console.WriteLine("Note: When prompted to enter Y or N, values beside Y or y are treated as no.\n");

            while (true)
            {
                Console.WriteLine("Quantity: ");
                var quantityAsString = Console.ReadLine();

                int quantity;
                while (!int.TryParse(quantityAsString, out quantity) || quantity < 1)
                {
                    Console.WriteLine("Please enter a positive integer value for quantity:");
                    quantityAsString = Console.ReadLine();
                }

                Console.WriteLine("Name: ");
                var name = Console.ReadLine();

                while (String.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Please enter a valid name:");
                    name = Console.ReadLine();
                }

                Console.WriteLine("Price: ");
                var priceAsString = Console.ReadLine();

                decimal price;
                while (!decimal.TryParse(priceAsString, out price) || price < 0)
                {
                    Console.WriteLine("Please enter a positive decimal value for price:");
                    priceAsString = Console.ReadLine();
                }

                Console.WriteLine("Taxable? (Y/N) (Enter y unless the item is a book, food, or medical product.)");
                var isTaxable = String.Equals(Console.ReadLine(), "y", StringComparison.OrdinalIgnoreCase)
                    ? true
                    : false;

                Console.WriteLine("Imported? (Y/N)");
                var isImported = String.Equals(Console.ReadLine(), "y", StringComparison.OrdinalIgnoreCase)
                    ? true
                    : false;

                Console.WriteLine("Add another item? (Y/N)");
                var input = Console.ReadLine()?.ToLower();

                var product = new Product(name, price, isTaxable, isImported);
                Cart.AddItem(product, quantity);

                Console.WriteLine();

                if (input == "y")
                    continue;
                break;
            }
        }

        private static void GenerateReceipt()
        {
            foreach (var cartLine in Cart.Lines)
            {
                Console.Write($"{cartLine.Product.Name}: {cartLine.TotalPrice:0.00}");
                if (cartLine.Quantity > 1)
                {
                    Console.Write($" ({cartLine.Quantity} @ {cartLine.ItemPrice:0.00})");
                }
                Console.Write("\n");
            }
            Console.WriteLine("Sales Taxes: {0:0.00}", Cart.ComputeTotalTaxes());
            Console.WriteLine("Total: {0:0.00}", Cart.ComputeTotalValue());
        }
    }
}