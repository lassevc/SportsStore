<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CartView.aspx.cs" Inherits="SportsStore.Pages.CartView" MasterPageFile="~/Pages/Store.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="content">
        <h2>Your cart</h2>
        <table id="cartTable">
            <thead>
                <tr>
                    <th>Quantity</th>
                    <th>Item</th>
                    <th>Price</th>
                    <th>Subtotal</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ItemType="SportsStore.Models.CartLine" SelectMethod="GetCartLines" runat="server" EnableViewState="false">
                    <ItemTemplate>
                        <tr>
                            <td><%# Item.Quantity %></td>
                            <td><%# Item.Product.Name %></td>
                            <td><%# Item.Product.Price %></td>
                            <td><%# (Item.Quantity * Item.Product.Price).ToString("c") %></td>
                            <td>
                                <button type="submit" class="actionButtons" name="remove" value="<%# Item.Product.ProductID %>">Remove</button>
                                <% // The view state caches the repeater (the items) in hidden HTML, and when clicking the remove button the items doesn't disappear
                                   // The CartTotal is updated, as it isn't a part of the repeater
                                   // To update the repeater, we have to disable view state, so this (not all) repeater control doesn't use view state data 
                                   // See in the asp:repeater - EnableViewState="false" %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="3">Total:</td>
                    <td><%= CartTotal.ToString("c") %></td>
                </tr>
            </tfoot>
        </table>
        <p class="actionButtons">
            <% // From code-behind - property getting url from SessionHelper: %>
            <a href="<%= ReturnUrl %>">Continue shopping</a>
            <% // From code-behind - property getting url from RouteConfig %>
            <a href="<%= CheckOutUrl %>">Checkout</a>
        </p>
    </div>
</asp:Content>
