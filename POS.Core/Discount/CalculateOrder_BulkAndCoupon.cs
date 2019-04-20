namespace POS.Core.Services
{
    public class CalculateOrder_BulkAndCoupon : CalculateDiscountDecorator
    {

        public Order Order { get; set; }
        public int BulkQty { get; set; }
        public int FreeQty { get; set; }
        public decimal EligibleAmt { get; set; }
        public decimal OffAmt { get; set; }

        private CalculateOrder_BulkAndCoupon()
        {
        }

        public CalculateOrder_BulkAndCoupon(Order order, int bulkQty, int freeQty, decimal eligibleAmt, decimal offAmt)
        {
            Order = order;
            EligibleAmt = eligibleAmt;
            OffAmt = offAmt;
            BulkQty = bulkQty;
            FreeQty = freeQty;
        }

        public override decimal CalculateDiscountPrice()
        {
            var total = new CalculateOrder_Bulk(Order, BulkQty, FreeQty).CalculateDiscountPrice();
            return new CalculateOrder_Coupon(total, EligibleAmt, OffAmt).CalculateDiscountPrice();
        }
    }
}