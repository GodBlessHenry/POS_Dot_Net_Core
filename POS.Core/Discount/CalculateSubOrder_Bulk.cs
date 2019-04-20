namespace POS.Core.Services
{
    public class CalculateSubOrder_Bulk
    {
        public OrderItem SubOrder;
        public int BulkQty { get; set; }
        public int FreeQty { get; set; }
        public int BulkAndFree => BulkQty + FreeQty;

        private CalculateSubOrder_Bulk()
        {
        }

        public CalculateSubOrder_Bulk(OrderItem subOrder, int bulkQty, int freeQty)
        {
            BulkQty = bulkQty;
            FreeQty = freeQty;
            SubOrder = subOrder;
        }

        public decimal CalculateDiscountPrice()
        {
            if (SubOrder.Quantity < BulkAndFree) return SubOrder.Quantity * SubOrder.Product.Price;

            var remainder = SubOrder.Quantity % BulkAndFree;
            var discountAmount = (SubOrder.Quantity - remainder) / BulkAndFree;

            return (discountAmount * BulkQty + remainder) * SubOrder.Product.Price;
        }
    }
}