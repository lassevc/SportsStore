using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using SportsStore.Models;
using SportsStore.Models.Repository;

namespace SportsStore.Pages.Admin
{
    public partial class Orders : System.Web.UI.Page
    {
        private Repository repo = new Repository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                int dispatchID;
                // Which button was pressed?
                if (int.TryParse(Request.Form["dispatch"], out dispatchID))
                {
                    Order myOrder = repo.Orders
                        .Where(o => o.OrderId == dispatchID)
                        .FirstOrDefault();
                    if (myOrder != null)
                    {
                        myOrder.Dispatched = true;
                        repo.SaveOrder(myOrder);
                    }
                }
            }
        }

        // Total method called from data-binding code nuggets in the web form
        public decimal Total(IEnumerable<OrderLine> orderLines)
        {
            decimal total = 0;
            foreach (OrderLine ol in orderLines)
            {
                total += ol.Product.Price * ol.Quantity;
            }
            return total;
        }

        // The method used for the Repeater in the web form
        // bool parameter has a Control attribute, which tells the ASP.NET Framework to get the value from a control - the showDispatched-control (Checkbox)
        public IEnumerable<Order> GetOrders([Control] bool showDispatched)
        {
            if (showDispatched)
            {
                return repo.Orders;
            }
            else
            {
                return repo.Orders.Where(o => !o.Dispatched);
            }
        }
    }
}