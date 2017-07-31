<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="SportsStore.Pages.Listing" MasterPageFile="~/Pages/Store.Master" %>

<asp:Content ContentPlaceHolderID="bodyContent" runat="server">
    <% // Applying the master page to this page using the MasterPageFile attribute
       // And removing the html/head/body-tags but inserting the asp:Content control - the ID matches the master page
       // For URL Routing we are importing the System.Web.Routing namespace %>

    <div id="content">
        <% // This server control produces the same as the user control just below
           // The Repeater control is used to generate the same set of elements for every item in a set of data object.
           // The ItemType and SelectMethod attributes are part of a new data-binding feature in ASP.NET 4.5 that is referred to as strongly typed data controls. %>
        <asp:Repeater ItemType="SportsStore.Models.Product" SelectMethod="GetProducts" runat="server">
            <ItemTemplate>
                <div class="item">
                    <h3><%# Item.Name %></h3>
                    <%# Item.Description %>
                    <button name="add" type="submit" value="<%# Item.ProductID %>">Add to Cart</button>
                    <h4><%# Item.Price.ToString("c") %></h4>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <% // User control
           /*
            foreach (SportsStore.Models.Product prod in GetProducts())
            {
                Response.Write("<div class='item'>");
                Response.Write(string.Format($"<h3>{prod.Name}</h3>"));
                Response.Write(prod.Description);
                // :c formatter renders numerical values as currency according to the culture settings - change culture settings in Web.config
                Response.Write(string.Format($"<button name='add' type='submit' value={prod.ProductID}'>Add to Cart</button>"));
                Response.Write(string.Format($"<h4>{prod.Price:c}</h4>"));
                Response.Write("</div>");
            }
            */
        %>
        <div class="pager">
            <%
                for (int i = 1; i <= MaxPage; i++)
                {
                    // Links to other pages with URL Routing (category and page) and mark for selected page (class='selected')
                    Response.Write(CreatePageLinkHtml(i));
                }
            %>
        </div>
    </div>
</asp:Content>
