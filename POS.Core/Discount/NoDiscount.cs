using System.Linq;

namespace POS.Core.Discount
{
    // This is the Decorator subclass
    public class NoDiscount : PriceAdjustment
    {
        public NoDiscount(IFinalPrice adjustedCart) : base(adjustedCart)
        {
        }

        // This function is to calculate regular total for the order
        public void CalculatePriceWithoutDiscount()
        {
            var adjustment = AdjustedCart
                            .Order
                            .OrderItems
                            .Sum(item => item.Quantity * item.Product.Price);

            this.AdjustedCart.AdjustPrice(-1 * adjustment);
        }
    }
}