using System.Linq;
using SalesTaxes.Models;
using Xunit;

namespace SalesTaxes.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            // Arrange - create some test products
            Product p1 = new Product("Book", 12.49m, false, false);
            Product p2 = new Product("Music CD", 14.99m, true, false);

            // Arrange - create a new cart
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange - create some test products
            Product p1 = new Product("Book", 12.49m, false, false);
            Product p2 = new Product("Music CD", 14.99m, false, false);

            // Arrange - create a new cart
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = target.Lines
                .OrderBy(c => c.Product.Name).ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Adds_New_Lines_For_Non_Identical_Products()
        {
            // Arrange - create some test products
            Product p1 = new Product("Book", 12.49m, false, false);
            Product p2 = new Product("Book", 12.50m, true, false);
            Product p3 = new Product("Book", 12.50m, true, true);
            Product p4 = new Product("Book", 12.50m, false, false);

            // Arrange - create a new cart
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p3, 1);
            target.AddItem(p4, 1);
            CartLine[] results = target.Lines.ToArray();

            // Assert
            Assert.Equal(4, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
            Assert.Equal(p3, results[2].Product);
            Assert.Equal(p4, results[3].Product);
        }

        [Fact]
        public void Calculate_Cart_Taxes()
        {
            // Arrange - create some test products
            Product p1 = new Product("Imported box of chocolates", 10.00m, false, true);
            Product p2 = new Product("Imported bottle of perfume", 47.50m, true, true);

            // Arrange - create a new cart
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            decimal taxes = target.ComputeTotalTaxes();

            // Assert
            Assert.Equal(7.65m, taxes);
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            // Arrange - create some test products
            Product p1 = new Product("Book", 12.49m, false, false);
            Product p2 = new Product("Music CD", 14.99m, true, false);
            Product p3 = new Product("Chocolate bar", 0.85m, false, false);

            // Arrange - create a new cart
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p3, 1);
            decimal total = target.ComputeTotalValue();

            // Assert
            Assert.Equal(42.32m, total);
        }
    }
}