using POS.Core.Service;

namespace POS.Core.Services
{
    public class CalculateOrder_Coupon : CalculateDiscountDecorator
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDiscountVariables _discountVariables;
        private decimal EligibleAmt => _discountVariables.EligibleAmt;
        private decimal OffAmt => _discountVariables.OffAmt;

        public CalculateOrder_Coupon(IDiscountVariables discountVariables, IOrderRepository orderRepository)
        {
            _discountVariables = discountVariables;
            _orderRepository = orderRepository;
        }

        public override decimal CalculateDiscountPrice()
        {
            var order = _orderRepository.GetById();
            var total = BaseDiscountCalculator.CalculateDiscountPrice();
            return (order.WithCoupon && total >= EligibleAmt) ? (total - OffAmt) : total;
        }
    }
}