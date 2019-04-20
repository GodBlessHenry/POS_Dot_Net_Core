using System.Linq;

namespace POS.Core.Services
{
    public class CalculateOrder_NoDiscount : CalculateDiscountDecorator
    {
        public Order Order { get; set; }

        private CalculateOrder_NoDiscount()
        {
        }

        public CalculateOrder_NoDiscount(Order order)
        {
            Order = order;
        }

        public override decimal CalculateDiscountPrice()
        {
            return Order.OrderItems.Sum(x => x.Product.Price * x.Quantity);
        }
    }
}