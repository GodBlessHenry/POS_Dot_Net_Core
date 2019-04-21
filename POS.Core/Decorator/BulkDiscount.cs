namespace POS.Core.Decorator
{
    public class BulkDiscount : PriceAdjustment
    {
        private const int BulkQty = 2;
        private const int FreeQty = 1;
        public int BulkAndFree => BulkQty + FreeQty;

        public BulkDiscount(IFinalPrice adjustedCart) : base(adjustedCart)
        {
        }

        public void AdjustPriceByBulkDiscount()
        {
            var adjustment = 0d;

            foreach (var item in AdjustedCart.Order.OrderItems)
            {
                adjustment += CalculateOrderItemPrice(item);
            }

            this.AdjustedCart.AdjustPrice(adjustment);
        }

        private double CalculateOrderItemPrice(OrderItem item)
        {
            if (item.Quantity < BulkAndFree || !item.Product.CanUseBulkDiscount)
                return 0;

            var remainder = item.Quantity % BulkAndFree;
            var freeCount = (item.Quantity - remainder) / BulkAndFree;

            return freeCount * item.Product.Price;
        }
    }
}