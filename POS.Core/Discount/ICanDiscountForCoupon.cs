namespace POS.Core.Services
{
    public interface ICanDiscountForCoupon 
    {
        decimal EligibleAmt { get; set; }
        decimal OffAmt { get; set; }

        decimal CalculateTotalOrderPrice(decimal total);
    }
}