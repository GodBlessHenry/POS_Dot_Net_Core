using System.Collections.Generic;

namespace POS.Core.Tests.Builders
{
    public class OrderBuilder : Builder<Order>
    {
        public OrderBuilder()
        {
            Creation = new Order
            {
                OrderItems = new List<OrderItem>
                {
                    Build.OrderItem()
                },
                WithCoupon = true
            };
        }

        public OrderBuilder WithCoupon(bool withCoupon)
        {
            Creation.WithCoupon = withCoupon;
            return this;
        }

        public OrderBuilder WithOrderItem(List<OrderItem> orderItems)
        {
            Creation.OrderItems = orderItems;
            return this;
        }
    }
}