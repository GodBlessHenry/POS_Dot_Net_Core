namespace POS.Core.Services
{
    public class CalculateOrderTotal
    {
        private readonly Order _order;
        private const int BulkQty = 2;
        private const int FreeQty = 1;
        private const decimal EligibleAmt = 100m;
        private const decimal OffAmt = 5m;

        //private IDiscount _discount;

        public CalculateOrderTotal(Order order) //, IDiscount discount)
        {
            _order = order;
            //_discount = discount;
        }

        //public decimal Calculate()
        //{
        //    var total = 0m;

        //    total = new CalculateOrder_Bulk(_order, 2, 1).CalculateDiscountPrice();
        //    total += new CalculateOrder_Coupon(total, 100, 5).CalculateDiscountPrice();

        //    return total;
        //}

        public decimal CalculateWithBulkDiscount()
        {
            CalculateOrder_Bulk bulk = new CalculateOrder_Bulk(_order, BulkQty, FreeQty);

            return bulk.CalculateDiscountPrice();
        }

        public decimal CalculateWithBulkAndCouponDiscount()
        {
            var coupon = new CalculateOrder_Coupon(_order, EligibleAmt, OffAmt);
            coupon.SetDiscount(new CalculateOrder_Bulk(_order, BulkQty, FreeQty));
            
            return coupon.CalculateDiscountPrice();
        }

        public decimal CalculateWithCouponDiscount()
        {
            var coupon = new CalculateOrder_Coupon(_order, EligibleAmt, OffAmt);
            coupon.SetDiscount(new CalculateOrder_NoDiscount(_order));

            return coupon.CalculateDiscountPrice();
        }

    }
}