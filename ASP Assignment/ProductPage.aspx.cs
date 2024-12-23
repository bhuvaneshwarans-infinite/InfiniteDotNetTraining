using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AssignmentASP
{
    public partial class ProductPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProductImages.Visible = false;
                lblPrice.Visible = false;
            }
        }

        protected void ProductsIndexChanged(object sender, EventArgs e)
        {
            int selectedProduct = int.Parse(Products.SelectedValue);

            ProductImages.Visible = false;
            lblPrice.Visible = false;

            switch (selectedProduct)
            {
                case 1:
                    ProductImages.ImageUrl = "images/product1.jpg";
                    ProductImages.Visible = true;
                    lblPrice.Visible = true;
                    lblPrice.Text = "Price: $890";
                    break;
                case 2:
                    ProductImages.ImageUrl = "images/product2.jpg";
                    ProductImages.Visible = true;
                    lblPrice.Visible = true;
                    lblPrice.Text = "Price: $200";
                    break;
                case 3:
                    ProductImages.ImageUrl = "images/product3.jpg";
                    ProductImages.Visible = true;
                    lblPrice.Visible = true;
                    lblPrice.Text = "Price: $700";
                    break;
                case 4:
                    ProductImages.ImageUrl = "images/product4.jpg";
                    ProductImages.Visible = true;
                    lblPrice.Visible = true;
                    lblPrice.Text = "Price: $1300";
                    break;
                default:
                    ProductImages.Visible = false;
                    lblPrice.Visible = false;
                    break;
            }           
        }

        protected void btnGetPrice_Click(object sender, EventArgs e)
        {
            lblPrice.Visible = true;
        }
    }
}