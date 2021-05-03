namespace SalesTaxes.Models
{
    public class Product
    {
        public string Name { get; }

        public decimal Price { get; }

        public bool Taxable { get; }

        public bool Imported { get; }

        public Product(string name, decimal price, bool taxable, bool imported)
        {
            Name = name;
            Price = price;
            Taxable = taxable;
            Imported = imported;
        }
    }
}