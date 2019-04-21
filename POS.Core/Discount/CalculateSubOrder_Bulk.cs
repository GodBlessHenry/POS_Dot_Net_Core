using POS.Core.Service;

namespace POS.Core.Services
{
    public class CalculateSubOrder_Bulk : ICalculateDiscount
    {
        private readonly IDiscountVariables _discountVariables;
        private readonly IOrderRepository _orderRepository;
        // if the discount is buy 2 get 1 free, then BulkQty = 2, FreeQty = 1, BulkAndFree = 3
        public int BulkQty { get; set; }
        public int FreeQty { get; set; }
        public int BulkAndFree => BulkQty + FreeQty;
        

        public CalculateSubOrder_Bulk(IDiscountVariables discountVariables, IOrderRepository orderRepository, IDiscountVariables discountVariables1, IOrderRepository orderRepository1)
        {
            _discountVariables = discountVariables1;
            _orderRepository = orderRepository1;
        }

        public decimal CalculateDiscountPrice()
        {

            if (SubOrder.Quantity < BulkAndFree || !SubOrder.Product.CanUseBulkDiscount)
                return SubOrder.Quantity * SubOrder.Product.Price;

            var remainder = SubOrder.Quantity % BulkAndFree;
            var freeCount = (SubOrder.Quantity - remainder) / BulkAndFree;

            return (SubOrder.Quantity - freeCount) * SubOrder.Product.Price;
        }
    }
}