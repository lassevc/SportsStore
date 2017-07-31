<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryList.ascx.cs" Inherits="SportsStore.Controls.CategoryList" %>
<% // Control directives means we are dealing with an user control %>

<% // This user control is inserted in the Store.Master-file %>

<% // Create the Home-link, which shows all the products (no "category" in URL) %>
<%=CreateHomeLinkHtml() %>

<% // Create the Category-links, which shows only the products in the selected category) %>
<% foreach (string cat in GetCategories())
    {
        Response.Write(CreateLinkHtml(cat));
    } %>