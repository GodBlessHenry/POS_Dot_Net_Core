namespace POS.Core.Decorator
{
    public class CouponDiscount : PriceAdjustment
    {
        private const double EligibleAmt = 100;
        private const double OffAmt = 5;
        private const double NoCouponAmt = 0d;

        public CouponDiscount(IFinalPrice adjustedCart) : base(adjustedCart)
        {
            Order = adjustedCart.Order;
        }

        public void AdjustPriceByCouponDiscount()
        {
            var total = AdjustedCart.GetFinalPrice();
            var adjustment = (Order.WithCoupon && total >= EligibleAmt) ? OffAmt : NoCouponAmt;
            //return (Order.WithCoupon && total >= EligibleAmt) ? (total - OffAmt) : total;
            
            this.AdjustedCart.AdjustPrice(adjustment);
        }
    }
}