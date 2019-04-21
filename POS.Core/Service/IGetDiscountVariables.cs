namespace POS.Core.Service
{
    public interface IDiscountVariables
    {
        int BulkQty { get; set; }
        int FreeQty { get; set; }
        decimal EligibleAmt { get; set; }
        decimal OffAmt { get; set; }
    }
}