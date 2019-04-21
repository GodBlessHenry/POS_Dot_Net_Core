using System.Collections.Generic;

namespace POS.Core
{
    public class Order 
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public bool WithCoupon { get; set; }

    }
}