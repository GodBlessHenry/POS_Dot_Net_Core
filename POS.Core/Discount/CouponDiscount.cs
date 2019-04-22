using POS.Core.Service;

namespace POS.Core.Discount
{
    // This is the Decorator subclass
    public class CouponDiscount : PriceAdjustment
    {
        private readonly IDiscountVariables _discountVariables;
        // For example, if it's over $100 get $5 off coupon,
        // then EligibleAmt = 100, OffAmt = $5
        private double EligibleAmt => _discountVariables.EligibleAmt;
        private double OffAmt => _discountVariables.OffAmt;
        private const double NoCouponAmt = 0d;

        public CouponDiscount(IFinalPrice adjustedCart, IDiscountVariables discountVariables) : base(adjustedCart)
        {
            _discountVariables = discountVariables;
        }

        // This function is to calculate coupon discount adjustment for the order
        public void AdjustPriceByCouponDiscount()
        {
            var total = AdjustedCart.GetFinalPrice();
            var adjustment = (AdjustedCart.Order.WithCoupon && total >= EligibleAmt) ? OffAmt : NoCouponAmt;
            
            this.AdjustedCart.AdjustPrice(adjustment);
        }
    }
}