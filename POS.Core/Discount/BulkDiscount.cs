using System.Linq;
using POS.Core.Service;

namespace POS.Core.Discount
{
    // This is the Decorator subclass
    public class BulkDiscount : PriceAdjustment
    {
        // for example, if it's buy 2 and get the 3rd one free,
        // then BulkQty = 2, FreeQty = 1
        private readonly IDiscountVariablesRepository _discountVariablesRepository;
        private int BulkQty => _discountVariablesRepository.BulkQty;
        private int FreeQty => _discountVariablesRepository.FreeQty;
        public int BulkAndFree => BulkQty + FreeQty;

        public BulkDiscount(IFinalPrice adjustedCart, IDiscountVariablesRepository discountVariablesRepository) : base(adjustedCart)
        {
            _discountVariablesRepository = discountVariablesRepository;
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