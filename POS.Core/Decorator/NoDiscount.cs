namespace POS.Core.Decorator
{
    public class NoDiscount : PriceAdjustment
    {
        public NoDiscount(IFinalPrice adjustedCart) : base(adjustedCart)
        {
            Order = adjustedCart.Order;
        }

        public void CalculatePriceWithoutDiscount()
        {
            var adjustment = 0d;

            foreach (var item in AdjustedCart.Order.OrderItems)
            {
                adjustment += item.Quantity * item.Product.Price;
            }

            this.AdjustedCart.AdjustPrice(-1 * adjustment);
        }
    }
}