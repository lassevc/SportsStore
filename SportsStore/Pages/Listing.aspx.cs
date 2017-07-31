using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SportsStore.Models;       // Add model-class
using SportsStore.Models.Repository;        // Add repository-class
using SportsStore.Pages.Helpers;
using System.Web.Routing;

namespace SportsStore.Pages
{
    public partial class Listing : System.Web.UI.Page
    {
        private Repository repo = new Repository(); // Create instance (connect to database)
        private int pageSize = 4;   // Number of products at each page

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                int selectedProductId;
                // We locate the id value of the required product from the form data that we receive - button "Add to cart" has name "add" and value productID
                if (int.TryParse(Request.Form["add"], out selectedProductId))
                {
                    // use the repository to retrieve the corresponding Product object
                    Product selectedProduct = repo.Products
                        .Where(p => p.ProductID == selectedProductId).FirstOrDefault();

                    if (selectedProduct != null)
                    {
                        // Using the SessionHelper class, we obtain the Cart associated with the user’s session and add the selected product to it
                        SessionHelper.GetCart(Session).AddItem(selectedProduct, 1);
                        SessionHelper.Set(Session, SessionKey.RETURN_URL, Request.RawUrl);

                        // We finish responding to the post by redirecting the user to another URL using the Response.Redirect method
                        // The URL that we redirect the browser to is generated from the routing configuration—there are still a lot of nulls
                        // when we generate the URL, but this version of the GetVirtualPath method creates a URL from a route called cart
                        // Addition to the /App_Start/RouteConfig.cs file makes this possible
                        Response.Redirect(RouteTable.Routes.GetVirtualPath(null, "cart", null).VirtualPath);
                    }
                }
            }
        }

        // Find products in database
        // Methods used with the SelectMethod attribute on server controls must be public (and not protected)
        public IEnumerable<Product> GetProducts()
        {
            return FilterProducts()                     // Finds products in category (or all)
                .OrderBy(p => p.ProductID)              // Using LINQ to take the next products for the current page
                .Skip((CurrentPage -1) * pageSize)      // Removes the first products, if the current page is higher than 1
                .Take(pageSize);                        // Take only the defined number of products
        }

        // Property - gets the page value in the URL
        // Example: http://localhost:53096/Pages/Listing.aspx?page=2 or http://localhost:53096/List/2
        protected int CurrentPage
        {
            get
            {
                int page = GetPageFromRequest();
                return page > MaxPage ? MaxPage: page;         // if page is to large (doesn't contain products), return last page instead
            }
        }

        // Property - gets the current category value in the URL
        protected string CurrentCategory
        {
            get
            {
                return (string)RouteData.Values["category"] ?? Request.QueryString["category"];
            }
        }

        // Property - finds the max page number (last page with products)
        protected int MaxPage
        {
            get
            {
                // Fint the current products - filtered by category if one exists
                int prodCount = FilterProducts().Count();
                return (int)Math.Ceiling((decimal)prodCount / pageSize);
            }
        }

        // If category is selected (routing variable or normal request), return only products with that category - else return them all
        private IEnumerable<Product> FilterProducts()
        {
            IEnumerable<Product> products = repo.Products;
            return CurrentCategory == null ? products : products.Where(p => p.Category == CurrentCategory);
        }

        private int GetPageFromRequest()
        {
            int page = 1;
            // We get the routing variable through the RouteData.Values collection - or from normal Request, if there isn't a routing variable available
            string reqValue = (string)RouteData.Values["page"] ?? Request.QueryString["page"];
            if (reqValue != null)
            {
                int.TryParse(reqValue, out page);
            }
            return page;
        }

        protected string CreatePageLinkHtml(int pageNum)
        {
            string path = RouteTable.Routes.GetVirtualPath(null, null, new RouteValueDictionary() { { "category", CurrentCategory }, { "page", pageNum } }).VirtualPath;
            string selectedClass = "";
            if (pageNum == CurrentPage) { selectedClass = "class='selected'"; }
            // Links to other pages with URL Routing and mark for selected page (class='selected')
            return string.Format($"<a href='{path}' {selectedClass}>{pageNum}</a> ");
        }
    }
}