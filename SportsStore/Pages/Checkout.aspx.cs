using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using SportsStore.Models;
using SportsStore.Models.Repository;
using SportsStore.Pages.Helpers;

namespace SportsStore.Pages
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // When the Visible property is set to false, the ASP.NET Framework doesn’t include the element or its content in the response sent to the browser
            // Unlike JavaScript which manipulates the HTML DOM in the browser (just hides the elements without removing them)
            // So: When you use the Visible property in a code-behind class, the elements are not sent at all.
            checkoutForm.Visible = true;
            checkoutMessage.Visible = false;

            if (IsPostBack)
            {
                Order myOrder = new Order();
                // we use model binding to create an Order object from the data that the user has submitted in the form
                if (TryUpdateModel(myOrder, new FormValueProvider(ModelBindingExecutionContext)))
                {
                    myOrder.OrderLines = new List<OrderLine>();

                    // we get the Cart from the Session data
                    Cart myCart = SessionHelper.GetCart(Session);

                    // we create an OrderLine object for each CartLine that the user has created
                    foreach (CartLine line in myCart.Lines)
                    {
                        myOrder.OrderLines.Add(new OrderLine
                        {
                            Order = myOrder,
                            Product = line.Product,
                            Quantity = line.Quantity
                        });
                    }

                    // We then store the Order in the repository using the SaveOrder method
                    new Repository().SaveOrder(myOrder);

                    // We clear the contents of the Cart object when we have stored the new Order object in the repository
                    myCart.Clear();
                    
                    // We shows the checkout message
                    checkoutForm.Visible = false;
                    checkoutMessage.Visible = true;
                }
            }
        }
    }
}