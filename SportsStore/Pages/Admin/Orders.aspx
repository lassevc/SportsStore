<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" EnableViewState="false" Inherits="SportsStore.Pages.Admin.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="outerContainer">
        <table id="ordersTable">
            <tr>
                <th>Name</th>
                <th>City</th>
                <th>Items</th>
                <th>Total</th>
            </tr>
            <% // Go through the orders (type Order) provided from the GetOrders method in codebehind %>
            <asp:Repeater runat="server" SelectMethod="GetOrders" ItemType="SportsStore.Models.Order">
                <ItemTemplate>
                    <tr>
                        <td><%#: Item.Name %></td> <% // encoded data-binding - notice the colon : - ensure data is safe to display in the browser %>
                        <td><%#: Item.City %></td> <% // encoded data-binding - notice the colon : - ensure data is safe to display in the browser %>
                        <td><%# Item.OrderLines.Sum(ol => ol.Quantity) %></td> <% // LINQ %>
                        <td><%# Total(Item.OrderLines).ToString("c") %></td> <% // Method with the Item.OrderLines as argument (code-behind) %>
                        <td>
                            <% // We are only showing the button, if the order hasn't been been dispatched
                               // Setting Visible value to the opposite of the dispatch value from Item (dispatched = true - visible = false) %>
                            <asp:PlaceHolder Visible="<%# !Item.Dispatched %>" runat="server">
                                <button type="submit" name="dispatch" value="<%# Item.OrderId %>">Dispatch</button>
                            </asp:PlaceHolder>
                        </td>
                    </tr>

                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>

    <div id="ordersCheck">
        <% // Using the Checkbox control instead of HTML element Input with type=Checkbox - the control has some useful features ex:
           // AutoPostBack-attribute=true - The Checkbox control adds some JavaScript, which will submit the form automatically when clicked %>
        <asp:CheckBox ID="showDispatched" runat="server" Checked="false" AutoPostBack="true" Text="Show Dispatched Orders?" />
    </div>

</asp:Content>