using System;

namespace POS.Core.Tests.Builders
{
    // This is for re-using the creation of the stubs.
    public class Build
    {
        private static readonly Random Rand = new Random();

        public static int RandomInt() => Rand.Next(1, 1000);

        public static string RandomString() => Guid.NewGuid().ToString();

        public static double RandomDouble() => (double)Rand.NextDouble();

        public static OrderBuilder Order()
        {
            return new OrderBuilder();
        }

        public static OrderItemBuilder OrderItem()
        {
            return new OrderItemBuilder();
        }

        public static ProductBuilder Product()
        {
            return new ProductBuilder();
        }
    }
}
