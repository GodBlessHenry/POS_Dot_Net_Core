using System.Linq;
using POS.Core.Service;

namespace POS.Core.Services
{
    public class CalculateOrder_NoDiscount : CalculateDiscountDecorator
    {
        private readonly IOrderRepository _orderRepository;

        public CalculateOrder_NoDiscount(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public override double CalculateDiscountPrice()
        {
            var order = _orderRepository.GetById();
            return order.OrderItems.Sum(x => x.Product.Price * x.Quantity);
        }
    }
}