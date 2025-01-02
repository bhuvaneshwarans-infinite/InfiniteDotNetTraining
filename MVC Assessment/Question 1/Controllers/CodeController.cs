using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCAssessmentQuestion1.Models;

namespace MVCAssessmentQuestion1.Controllers
{
    public class CodeController : Controller
    {
        NorthWindEntities context = new NorthWindEntities();

        public ActionResult Index()
        {
            var Customers = context.Customers.ToList();
            return View(Customers);
        }

        public ActionResult GetCustomerByCountryGermany()
        {
            var Customers = context.Customers.ToList();
            var CustomersGermany = from data in Customers where (data.Country).Equals("Germany") select data;
            return View(CustomersGermany);
        }

        public ActionResult GetCustomerByOrderID10248()
        {
            var Customers = context.Customers.ToList();
            var Orders = context.Orders.ToList();

            var CustomersByOrderID = from data in Customers join order in Orders on data.CustomerID equals order.CustomerID
                                        where order.OrderID == 10248 select data;

            return View(CustomersByOrderID);

        }




    }
}