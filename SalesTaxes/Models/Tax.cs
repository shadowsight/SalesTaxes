using System;

namespace SalesTaxes.Models
{
    public static class Tax
    {
        private const double BasicTaxRate = .10;
        private const double ImportTaxRate = .05;
        private static decimal _tax = 0.00m;

        public static decimal CalculateTax(Product product)
        {
            if (product.Taxable && product.Imported)
            {
                _tax = CalculateFullTax(product.Price);
            }
            else if (product.Taxable)
            {
                _tax = CalculateBasicTax(product.Price);
            }
            else if (product.Imported)
            {
                _tax = CalculateImportTax(product.Price);
            }
            else
            {
                _tax = 0.00m;
            }
            return Math.Ceiling(_tax * 20) / 20;
        }

        private static decimal CalculateBasicTax(decimal price)
        {
            return price * (decimal)BasicTaxRate;
        }

        private static decimal CalculateImportTax(decimal price)
        {
            return price * (decimal)ImportTaxRate;
        }

        private static decimal CalculateFullTax(decimal price)
        {
            double totalTaxRate = BasicTaxRate + ImportTaxRate;

            return price * (decimal)totalTaxRate;
        }
    }
}