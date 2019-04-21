namespace POS.Core.Decorator
{
    public class CouponDiscount : PriceAdjustment
    {
        private const double EligibleAmt = 100;
        private const double OffAmt = 5;
        private const double NoCouponAmt = 0d;

        public CouponDiscount(IFinalPrice adjustedCart) : base(adjustedCart)
        {
        }

        public void AdjustPriceByCouponDiscount()
        {
            var total = AdjustedCart.GetFinalPrice();
            var adjustment = (AdjustedCart.Order.WithCoupon && total >= EligibleAmt) ? OffAmt : NoCouponAmt;
            
            this.AdjustedCart.AdjustPrice(adjustment);
        }
    }
}