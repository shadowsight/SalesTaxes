Author: Byron Wong
Created Date: 12/20/20

I created this solution with Visual Studio 2019, targetting .NET Core 3.1. 

The main project, SalesTaxes, contains the following classes.

Cart
Contains a class called CartLine to represent each shopping cart item of the exact same type.
Uses a List<CartLine> collection with LINQ to add or update each cart item, as well as to compute the total value and the total taxes.

Product
Consists of the properties Name, Price, Taxable, and Imported. The user gets to enter these values for each item along with the quantity.

Tax
Static class with a CalculateTax method that calculates the total tax for the given item. Taxes are rounded to the nearest 5 cents. 

Program
Accepts inputs from the user to add items to a shopping cart, then generates the output of the receipt.



An XUnit test project is also included and features tests that cover the functionalities of the Cart and Tax classes.