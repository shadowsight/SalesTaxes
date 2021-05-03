using SalesTaxes.Models;
using Xunit;

namespace SalesTaxes.Tests
{
    public class TaxTests
    {
        [Fact]
        public void Can_Calculate_Basic_Sales_Tax()
        {
            // Arrange - create some test products
            Product p1 = new Product("Music CD", 14.99m, true, false);
            Product p2 = new Product("Bottle of perfume", 18.99m, true, false);

            // Act
            decimal tax1 = Tax.CalculateTax(p1);
            decimal tax2 = Tax.CalculateTax(p2);

            // Assert
            Assert.Equal(1.50m, tax1);
            Assert.Equal(1.90m, tax2);
        }

        [Fact]
        public void Can_Calculate_Import_Tax()
        {
            // Arrange - create a test product
            Product p1 = new Product("Imported box of chocolates", 10.00m, false, true);

            // Act
            decimal tax = Tax.CalculateTax(p1);

            // Assert
            Assert.Equal(0.50m, tax);
        }

        [Fact]
        public void Can_Calculate_Basic_Sales_Tax_Combined_With_Import_Tax()
        {
            // Arrange - create a test product
            Product p1 = new Product("Imported bottle of perfume", 47.50m, true, true);

            // Act
            decimal tax = Tax.CalculateTax(p1);

            // Assert
            Assert.Equal(7.15m, tax);
        }

        [Fact]
        public void Does_Not_Tax_Exempt_And_Non_Import_Items()
        {
            // Arrange - create some test products
            Product p1 = new Product("Book", 12.49m, false, false);
            Product p2 = new Product("Chocolate bar", 0.85m, false, false);

            // Act
            decimal tax1 = Tax.CalculateTax(p1);
            decimal tax2 = Tax.CalculateTax(p2);

            // Assert
            Assert.Equal(0.00m, tax1);
            Assert.Equal(0.00m, tax2);
        }
    }
}