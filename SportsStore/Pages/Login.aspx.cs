using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace SportsStore.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string name = Request.Form["name"];
                string password = Request.Form["password"];
                // Check if name and password is set - and checks if it matches the user and password in Web.config file
                if (name != null && password != null && FormsAuthentication.Authenticate(name, password))
                {
                    // Sets a cookie, which will only last as long as the user's session (second parameter = false)
                    FormsAuthentication.SetAuthCookie(name, false);
                    Response.Redirect(Request["ReturnUrl"] ?? "/");
                }
                else
                {
                    // Shows the message in the ValidationSummary control
                    ModelState.AddModelError("fail", "Login failed. Please try again");
                }
            }
        }
    }
}