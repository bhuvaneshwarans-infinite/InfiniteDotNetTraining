<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductPage.aspx.cs" Inherits="AssignmentASP.ProductPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Display</title>
    <style>
        .productImage {
            width: 250px;
            height: 250px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Product Selection</h1>            

            <asp:DropDownList ID="Products" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ProductsIndexChanged">
                <asp:ListItem Text="Select product" Value="0" />
                <asp:ListItem Text="Product1" Value="1" />
                <asp:ListItem Text="Product2" Value="2" />
                <asp:ListItem Text="Product3" Value="3" />
                <asp:ListItem Text="Product4" Value="4" />
            </asp:DropDownList> 
            <br />
            <asp:Image ID="ProductImages" runat="server" CssClass="productImage" Visible="false" /> 
            <br />
            <asp:Button ID="btnGetPrice" runat="server" Text="Get Price" OnClick="btnGetPrice_Click" />            
            <br />
            <asp:Label ID="lblPrice" runat="server" Text="Price:" Visible="false" />
        </div>
    </form>
</body>
</html>
