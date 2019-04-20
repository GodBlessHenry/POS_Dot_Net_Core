using System;

namespace POS.Core
{
    public class Order
    {
        public int Id { get; protected set; }
        public string Description { get; protected set; }
        public string Name { get; protected set; }
        public decimal Amount { get; protected set; }
        public decimal Gst { get; protected set; }
        public DateTime OrderDateTime { get; protected set; }

        private Order()
        {
        }

        public Order(string name, decimal amount, decimal gst, DateTime orderDateTime)
        {
            Name = name;
            Amount = amount;
            Gst = gst;
            OrderDateTime = orderDateTime;
        }
    }
}
