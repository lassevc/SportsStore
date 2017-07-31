using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity; // DbContext

namespace SportsStore.Models.Repository
{
    public class EFDbContext : DbContext
    {
        // We want the Product model type (class) to be used to represent rows in the Products table (database)
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        // We don’t need to add a property for the OrderLines table because we won’t be working with it directly. The way that the Entity Framework
        // handles foreign key relationships means that OrderLine objects will be handled automatically through the Order objects they are associated with.

        // Remember to make the connection to the database in Web.config (connectionstring)
    }
}