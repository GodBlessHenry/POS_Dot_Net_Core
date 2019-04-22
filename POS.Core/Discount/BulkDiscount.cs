using System.Linq;
using POS.Core.Service;

namespace POS.Core.Discount
{
    // This is the Decorator subclass
    public class BulkDiscount : PriceAdjustment
    {
        // for example, if it's buy 2 get 1 free,
        // then BulkQty = 2, FreeQty = 1
        private readonly IDiscountVariables _discountVariables;
        private int BulkQty => _discountVariables.BulkQty;
        private int FreeQty => _discountVariables.FreeQty;
        public int BulkAndFree => BulkQty + FreeQty;

        public BulkDiscount(IFinalPrice adjustedCart, IDiscountVariables discountVariables) : base(adjustedCart)
        {
            _discountVariables = discountVariables;
        }

        public void AdjustPriceByBulkDiscount()
        {
            var adjustment = AdjustedCart
                            .Order
                            .OrderItems
                            .Sum(i => CalculateOrderItemPrice(i));

            AdjustedCart.AdjustPrice(adjustment);
        }

        // This function is to calculate bulk discount adjustment for each order item 
        private double CalculateOrderItemPrice(OrderItem item)
        {
            if (item.Quantity < BulkAndFree || !item.Product.CanUseBulkDiscount) return 0;

            var remainder = item.Quantity % BulkAndFree;
            var freeCount = FreeQty * (item.Quantity - remainder) / BulkAndFree;

            return freeCount * item.Product.Price;
        }
    }
}