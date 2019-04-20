namespace POS.Core.Services
{
    public class CalculateOrder_Bulk : CalculateDiscountDecorator
    {
        public Order Order { get; set; }
        public int BulkQty { get; set; }
        public int FreeQty { get; set; }

        private CalculateOrder_Bulk()
        {
        }

        public CalculateOrder_Bulk(Order order, int bulkQty, int freeQty)
        {
            BulkQty = bulkQty;
            FreeQty = freeQty;
            Order = order;
        }

        public override decimal CalculateDiscountPrice()
        {
            var total = 0m;

            foreach (var item in Order.OrderItems)
            {
                total += new CalculateSubOrder_Bulk(item, BulkQty, FreeQty).CalculateDiscountPrice();
            }

            return total;
        }
    }
}