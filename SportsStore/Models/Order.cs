using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        // Server-side validation - ValidationSummary in Checkout.aspx
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the first address line")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter a state")]
        public string State { get; set; }
        public bool GiftWrap { get; set; }
        public bool Dispatched { get; set; }
        public virtual List<OrderLine> OrderLines { get; set; }

        // Not ideal validation alone as it might take to long - supplement the server-side validation with client-side validation (JavaScript)
        // Use client-side validation to improve the user experience and server-side validation to protect your application.
    }

    public class OrderLine
    {
        public int OrderLineId { get; set; }
        
        // Entity Framework feature for expressing relationships between tables through object properties, which is why the OrderLine class defines
        // Product and Order properties, rather than int values, to hold keys for rows in the Products and Orders table.
        // The Entity Framework will automatically use the foreign keys to locate rows in the other tables and represent them with C# objects.
        public Order Order { get; set; }
        public Product Product { get; set; }

        // Similarly, applying the virtual keyword to the OrderLines property in the Order class causes the Entity Framework to
        // load all of the OrderLine rows that are associated with an order and represent them with a list of OrderLine objects.
        public int Quantity { get; set; }
    }
}