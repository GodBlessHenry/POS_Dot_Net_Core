namespace POS.Core.Service
{
    public class DiscountVariables : IDiscountVariables
    {
        public int BulkQty { get; set; }
        public int FreeQty { get; set; }
        public decimal EligibleAmt { get; set; }
        public decimal OffAmt { get; set; }

        public DiscountVariables()
        {
            // These values should be saved in database,
            // below just fake it like fetching them from the database 
            BulkQty = 2;
            FreeQty = 1;
            EligibleAmt = 100;
            OffAmt = 5;
        }
    }
}