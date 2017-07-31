using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        // Add product to cart
        public void AddItem(Product product, int quantity)
        {
            // Check if the product already is added to the cart
            CartLine line = lineCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();

            // If the product is not previously added, add the CartLine object (product and quantity)
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                // If already added, update the quantity
                line.Quantity += quantity;
            }
        }

        // Remove the current product from cart
        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        // Calculate the total price for all the products in the cart
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        // Reset the cart - removing all products
        public void Clear()
        {
            lineCollection.Clear();
        }

        // Property to give access to the content of the cart
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}