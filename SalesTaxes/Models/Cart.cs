using System.Collections.Generic;
using System.Linq;

namespace SalesTaxes.Models
{
    public class Cart
    {
        private readonly List<CartLine> _lineCollection = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = _lineCollection
                .FirstOrDefault(p =>
                    p.Product.Name == product.Name &&
                    p.Product.Price == product.Price &&
                    p.Product.Taxable == product.Taxable &&
                    p.Product.Imported == product.Imported);

            if (line == null)
            {
                _lineCollection.Add(new CartLine(
                    product, quantity));
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual decimal ComputeTotalValue() =>
            _lineCollection.Sum(e => e.ItemPrice * e.Quantity);

        public virtual decimal ComputeTotalTaxes() => _lineCollection.Sum(e => e.ItemTax * e.Quantity);

        public virtual IEnumerable<CartLine> Lines => _lineCollection;
    }

    public class CartLine
    {
        public decimal ItemTax { get; }
        public Product Product { get; }
        public decimal ItemPrice { get; }

        public decimal TotalPrice { get; }

        public int Quantity { get; set; }

        public CartLine(Product product, int quantity)
        {
            Product = product;
            Quantity += quantity;

            ItemTax = Tax.CalculateTax(product);
            ItemPrice = product.Price + ItemTax;

            TotalPrice = ItemPrice * Quantity;
        }
    }
}