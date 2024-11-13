using System;
using System.Collections.Generic;

//2.Create a Class called Products with Productid, Product Name, Price. Accept 10 Products,
//    sort them based on the price, and display the sorted Products

namespace Assessment2
{
    public class Products
    {
        private string productID;
        private string productName;
        private float productPrice;

        public Products()
        {

        }
        public Products(string productID, string productName, float productPrice)
        {
            this.productID = productID;
            this.productName = productName;
            this.productPrice = productPrice;
        }

        public string _Productid { get; set; }
        public string _ProductName { get; set; }
        public string _ProductPrice { get; set; }
        public override string ToString()
        {

            return "Product Name:"+ _Productid +" ,Product ID:"+ _ProductName + " and Product Price is "+_ProductPrice;
        }

    }
    class ProductOp : Products, IComparable<Products>
    {

        public Products[] product = new Products[10];
        public int CompareTo(Products prodObj)
        {
            if (prodObj != null)
            {
                return this._ProductPrice.CompareTo(prodObj._ProductPrice);
            }
            return -2;
        }

        public Products this[int index]
        {
            get { return product[index]; }
            set
            {
                product[index] = value;
            }
        }

        static void Main()
        {

            ProductOp product = new ProductOp();
            List<Products> list = new List<Products>();
            Console.WriteLine("ENter your 10 products");
            for (int i = 0; i < 10; i++)
            {

                Console.Write("\nEnter the ProductID:");
                string productID = Console.ReadLine();
                Console.Write("\nEnter the Product Name:");
                string productName = Console.ReadLine();
                Console.Write("\nEnter the Product price:");
                float productPrice = Convert.ToSingle(Console.ReadLine());
                product[i] = new Products(productID, productName, productPrice);
                list.Add(product[i]);
            }

            list.Sort();
       

            foreach (Products prodObj in list)
            {
                Console.WriteLine("Product Name:" + prodObj._Productid + " ,Product ID:" + prodObj._ProductName + " and Product Price is " + prodObj._ProductPrice);
            }
            Console.WriteLine("\nPress enter to exit");
            Console.Read();

        }
    }





}
