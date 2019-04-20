using Microsoft.EntityFrameworkCore;
using POS.Core;
using POS.Core.Services;

namespace POS.Storage.SqlServer
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        //public DbSet<Discount> Discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  // If we do not put the following code, the table names will be in plural, e.g. Products.
            modelBuilder.Entity<Order>().ToTable("Order");

            //modelBuilder.Entity<Customer>().HasAlternateKey(c => c.Email).HasName("AlternateKey_Email");
        }
    }
}
