using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Routing;
using SportsStore.Models.Repository;

namespace SportsStore.Controls
{
    // User control - code-behind
    public partial class CategoryList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected IEnumerable<string> GetCategories()
        {
            return new Repository().Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(x => x);
        }

        protected string CreateHomeLinkHtml()
        {
            string path = RouteTable.Routes.GetVirtualPath(null, null).VirtualPath;
            return string.Format($"<a href='{path}'>Home</a>");
        }

        protected string CreateLinkHtml(string category)
        {
            // Finds the selected category from the URL string
            string selectedCategory = (string)Page.RouteData.Values["category"] ?? Request.QueryString["category"];

            // Make the path URL with the category (from foreach in .ascx-file) and always page = 1
            string path = RouteTable.Routes.GetVirtualPath(null, null, new RouteValueDictionary() { { "category", category }, { "page", "1" } }).VirtualPath;

            string selectedClass = "";
            if (selectedCategory == category) { selectedClass = "class='selected'"; }

            return string.Format($"<a href='{path}' {selectedClass}>{category}</a>");
        }
    }
}