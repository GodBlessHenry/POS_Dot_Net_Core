using POS.Core.Service;

namespace POS.Core.Discount
{
    // This is the Decorator subclass
    public class CouponDiscount : PriceAdjustment
    {
        private readonly IDiscountVariablesRepository _discountVariablesRepository;
        // For example, if it's over $100 get $5 off coupon,
        // then EligibleAmt = 100, OffAmt = $5
        private double EligibleAmt => _discountVariablesRepository.EligibleAmt;
        private double OffAmt => _discountVariablesRepository.OffAmt;
        private const double NoCouponAmt = 0d;

        public CouponDiscount(IFinalPrice adjustedCart, IDiscountVariablesRepository discountVariablesRepository) : base(adjustedCart)
        {
            _discountVariablesRepository = discountVariablesRepository;
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