<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CartSummary.ascx.cs" Inherits="SportsStore.Controls.CartSummary" %>

<div id="cartSummary">
    <span class="caption">
        <b>Your cart:</b>
        <% // When the ASP.NET Framework processes a Web Form or user control, it creates variables for each HTML element with the runat attribute (code-behind)
           // The id attribute value is used as the variable name (csQuantity and csTotal represent the span elements, and csLink represents the a element)
            %>
        <span id="csQuantity" runat="server"></span> item(s),
        <span id="csTotal" runat="server"></span>
    </span>
    <a id="csLink" runat="server">Checkout</a>
</div>