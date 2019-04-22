namespace POS.Core.Service
{
    // This class is just to similate a repository class of
    // fetching the discount constants, instead of hardcoding
    // them inside the classes.
    public class DiscountVariablesRepository : IDiscountVariablesRepository
    {
        public int BulkQty { get; set; }
        public int FreeQty { get; set; }
        public double EligibleAmt { get; set; }
        public double OffAmt { get; set; }

        public DiscountVariablesRepository()
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