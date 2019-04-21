using POS.Core.Service;

namespace POS.Core.Services
{
    public class CalculateOrder_Bulk : CalculateDiscountDecorator
    {
        private readonly IDiscountVariables _discountVariables;
        private readonly IOrderRepository _orderRepository;
        private int BulkQty => _discountVariables.BulkQty;
        private int FreeQty => _discountVariables.FreeQty;
        public int BulkAndFree => BulkQty + FreeQty;

        public CalculateOrder_Bulk(IDiscountVariables discountVariables, IOrderRepository orderRepository)
        {
            _discountVariables = discountVariables;
            _orderRepository = orderRepository;
        }

        public override double CalculateDiscountPrice()
        {
            var total = 0d;
            var order = _orderRepository.GetById();

            foreach (var item in order.OrderItems)
            {
                total += CalculateOrderItemPrice(item);
            }

            return total;
        }

        private double CalculateOrderItemPrice(OrderItem item)
        {
            if (item.Quantity < BulkAndFree || !item.Product.CanUseBulkDiscount)
                return item.Quantity * item.Product.Price;

            var remainder = item.Quantity % BulkAndFree;
            var freeCount = (item.Quantity - remainder) / BulkAndFree;

            return (item.Quantity - freeCount) * item.Product.Price;
        }
    }
}