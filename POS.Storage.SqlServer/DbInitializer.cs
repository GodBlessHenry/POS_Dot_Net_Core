using System;
using System.Linq;
using POS.Core;

namespace POS.Storage.SqlServer
{
    public class DbInitializer
    {
        public static void Initialize(Context context)
        {
            context.Database.EnsureCreated();

            // Look for any Orders.
            if (context.Orders.Any())
            {
                return;   // DB has been seeded
            }

            var orders = new Order[]
            {
                new Order("Apple", 10m, 0m, DateTime.Now),
                new Order("Pear", 5m, 15m, DateTime.Now),
                new Order("Pineapple", 20m, 5m, DateTime.Now),
                new Order("Mango", 25m, 10m, DateTime.Now),
                new Order("Strawberry", 8.5m, 0m, DateTime.Now),
            };

            foreach (var p in orders)
            {
                context.Orders.Add(p);
            }

            context.SaveChanges();
        }
    }
}
