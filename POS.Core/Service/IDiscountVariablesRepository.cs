namespace POS.Core.Service
{
    public interface IDiscountVariablesRepository
    {
        int BulkQty { get; set; }
        int FreeQty { get; set; }
        double EligibleAmt { get; set; }
        double OffAmt { get; set; }
    }
}