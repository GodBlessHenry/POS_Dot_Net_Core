namespace POS.Core.Decorator
{
    public abstract class PriceAdjustment : IFinalPrice
    {
        protected IFinalPrice AdjustedCart;
        private double Adjustment { get; set; }
        public Order Order { get; set; }

        protected PriceAdjustment(IFinalPrice adjustedCart)
        {
            AdjustedCart = adjustedCart;
        }

        public void AdjustPrice(double adjustment)
        {
            AdjustedCart.AdjustPrice(adjustment);
        }

        public double GetFinalPrice()
        {
            return AdjustedCart.GetFinalPrice();
        }

        public double GetAdjustment()
        {
            return Adjustment;
        }
    }
}