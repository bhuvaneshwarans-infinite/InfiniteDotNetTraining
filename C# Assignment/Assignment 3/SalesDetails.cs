using Assigment3Solving;
using System;


//3.Create a class called Saledetails which has data members like Salesno, Productno, Price, dateofsale, Qty, TotalAmount
//- Create a method called Sales() that takes qty, Price details of the object and updates the TotalAmount as  Qty *Price
//- Pass the other information like SalesNo, Productno, Price, Qty and Dateof sale through constructor
//- call the show data method to display the values without an object.

namespace Assigment3Solving
{
    public class SalesDetails
    {
        protected internal string salesNo;
        protected internal string productNo;
        protected internal decimal price;
        protected internal DateTime dateOfSale;
        protected internal int qty;
        protected internal decimal totalAmount;

        public SalesDetails() { }

        public SalesDetails(string salesNo, string productNo, decimal price, int qty, DateTime dateOfSale)
        {
            this.salesNo = salesNo;
            this.productNo = productNo;
            this.price = price;
            this.qty = qty;
            this.dateOfSale = dateOfSale;
        }

     }

    class SalesOps : SalesDetails
    {
        public SalesOps(string salesNo, string productNo, decimal price, int qty, DateTime dateOfSale) :base(salesNo, productNo, price, qty, dateOfSale)
        {

        }
        public void Sales()
        {
            base.totalAmount = price * qty;
        }

        public static void ShowData(string salesNo, string productNo, decimal price, int qty, DateTime dateOfSale, decimal totalAmount)
        {
            Console.WriteLine("\n========Result=========");
            Console.WriteLine("Sales No: " + salesNo);
            Console.WriteLine("Product No: " + productNo);
            Console.WriteLine("Price: " + price);
            Console.WriteLine("Quantity: " + qty);
            Console.WriteLine("Date of Sale: " + dateOfSale.ToString("MM/dd/yyyy"));
            Console.WriteLine("Total Amount: " + totalAmount);
        }
    }


    class Sales
    {
        static void Main()
        {
            Console.Write("Enter the Sales no:");
            string Salesno = Console.ReadLine();
            Console.Write("Enter the Product no:");
            string Productno = Console.ReadLine();
            Console.Write("Enter the Price:");
            decimal Price =Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter the Date of Sale:");
            string dateofsale = Console.ReadLine();
            DateTime parsedDate;
            bool success = DateTime.TryParse(dateofsale, out parsedDate);

            if (success)
            {
                Console.WriteLine("Parsed Date: " + parsedDate);  
            }
            else
            {
                Console.WriteLine("Invalid date format.So default will be intialized");
                parsedDate = DateTime.MinValue;
            }
            Console.Write("Enter the Qty:");
            int Qty = Convert.ToInt32(Console.ReadLine());
            

            SalesOps sp = new SalesOps(Salesno, Productno, Price, Qty, parsedDate);
            sp.Sales();
            SalesOps.ShowData(Salesno, Productno, Price, Qty, parsedDate, sp.totalAmount);
            Console.Read();
        }
    }

}
