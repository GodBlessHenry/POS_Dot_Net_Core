using POS.Core.Service;

namespace POS.Core.Services
{
    public class CalculateOrderTotal
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDiscountVariables _discountVariables;

        public CalculateOrderTotal(IDiscountVariables discountVariables, IOrderRepository orderRepository)
        {
            _discountVariables = discountVariables;
            _orderRepository = orderRepository;
        }

        public decimal CalculateWithNoDiscount()
        {
            var bulk = new CalculateOrder_NoDiscount(_orderRepository);

            return bulk.CalculateDiscountPrice();
        }

        public decimal CalculateWithBulkDiscount()
        {
            var bulk = new CalculateOrder_Bulk(_discountVariables, _orderRepository);

            return bulk.CalculateDiscountPrice();
        }

        public decimal CalculateWithBulkAndCouponDiscount()
        {
            var coupon = new CalculateOrder_Coupon(_discountVariables, _orderRepository);
            coupon.SetBaseDiscount(new CalculateOrder_Bulk(_discountVariables, _orderRepository));

            return coupon.CalculateDiscountPrice();
        }

        public decimal CalculateWithCouponDiscount()
        {
            var coupon = new CalculateOrder_Coupon(_discountVariables, _orderRepository);
            coupon.SetBaseDiscount(new CalculateOrder_NoDiscount(_orderRepository));

            return coupon.CalculateDiscountPrice();
        }

    }
}