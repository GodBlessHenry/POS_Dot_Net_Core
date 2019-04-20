namespace POS.Core.Services
{
    public class CalculateOrder_Coupon : CalculateDiscountDecorator
    {
        public Order Order { get; set; }
        public decimal EligibleAmt { get; set; }
        public decimal OffAmt { get; set; }

        private CalculateOrder_Coupon()
        {
        }

        public CalculateOrder_Coupon(Order order, decimal eligibleAmt, decimal offAmt)
        {
            Order = order;
            EligibleAmt = eligibleAmt;
            OffAmt = offAmt;
        }

        public override decimal CalculateDiscountPrice()
        {
            var total = CalculateDiscount.CalculateDiscountPrice();
            return (Order.WithCoupon && total >= EligibleAmt) ? (total - OffAmt) : total;
        }
    }
}