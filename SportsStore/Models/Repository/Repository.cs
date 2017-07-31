using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SportsStore.Models.Repository
{
    // Repository class, which operates on the EFDbContext class and that acts as a bridge between our application business logic and the database
    public class Repository
    {
        private EFDbContext context = new EFDbContext();

        // Property called Products, which returns results of reading the property of the same name from the EFDbContext class
        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                product = context.Products.Add(product);
            }
            else
            {
                Product dbProduct = context.Products.Find(product.ProductID);
                if (dbProduct != null)
                {
                    dbProduct.Name = product.Name;
                    dbProduct.Description = product.Description;
                    dbProduct.Price = product.Price;
                    dbProduct.Category = product.Category;
                }
            }
            context.SaveChanges();
        }

        // We have to delete orders and orderlines with the current product to maintain the integrity of the database
        public void DeleteProduct(Product product)
        {
            // Find all orders and orderlines where the productID is in the orderline
            IEnumerable<Order> orders = context.Orders
                .Include(o => o.OrderLines.Select(ol => ol.Product))
                .Where(o => o.OrderLines.Count(ol => ol.Product.ProductID == product.ProductID) > 0)
                .ToArray();

            foreach (Order order in orders)
            {
                context.Orders.Remove(order);
            }
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public IEnumerable<Order> Orders
        {
            get
            {
                // Returns an enumeration of the rows in the Orders database table, where each is represented by an Order object.
                // The Include and Select methods ensure that we load the Product object associated with each OrderLine when querying the database
                return context.Orders.Include(o => o.OrderLines.Select(ol => ol.Product));
            }
        }

        public void SaveOrder(Order order)
        {
            // Store new Order object
            if (order.OrderId == 0)
            {
                order = context.Orders.Add(order);

                foreach (OrderLine line in order.OrderLines)
                {
                    context.Entry(line.Product).State = EntityState.Modified;
                }
            }
            else
            {
                // Modify existing Order object
                Order dbOrder = context.Orders.Find(order.OrderId);
                if (dbOrder != null)
                {
                    dbOrder.Name = order.Name;
                    dbOrder.Line1 = order.Line1;
                    dbOrder.Line2 = order.Line2;
                    dbOrder.Line3 = order.Line3;
                    dbOrder.City = order.City;
                    dbOrder.State = order.State;
                    dbOrder.GiftWrap = order.GiftWrap;
                    dbOrder.Dispatched = order.Dispatched;
                }
            }

            context.SaveChanges();
        }
    }
}