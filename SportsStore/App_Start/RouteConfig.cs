using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing; // Routing URLs

//namespace SportsStore.App_Start
namespace SportsStore
{
    public class RouteConfig
    {
        // Must be called, when the application starts - see Global.asax
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Remember: order is important
            // {page} = Routing segment variables:
            // The request like http://localhost:2000/list/2 is handled using the Listing.aspx page and a variable called page is created and assigned a value of 2
            // routes.MapPageRoute(null, "list/{page}", "~/Pages/Listing.aspx");
            // And now with category as well
            routes.MapPageRoute(null, "list/{category}/{page}", "~/Pages/Listing.aspx");
            routes.MapPageRoute(null, "", "~/Pages/Listing.aspx");
            routes.MapPageRoute(null, "list", "~/Pages/Listing.aspx");

            // This route adds support for a /cart URL, which is handled with the /Pages/CartView.aspx Web Form - The first argument specifies a name for the route
            routes.MapPageRoute("cart", "cart", "~/Pages/CartView.aspx");

            // Checkout link
            routes.MapPageRoute("checkout", "checkout", "~/Pages/Checkout.aspx");

            // Admin pages
            routes.MapPageRoute("admin_orders", "admin/orders", "~/Pages/Admin/Orders.aspx");
            routes.MapPageRoute("admin_products", "admin/products", "~/Pages/Admin/Products.aspx");
        }
    }
}