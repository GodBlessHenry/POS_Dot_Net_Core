namespace POS.Core.Discount
{
    public interface IFinalPrice
    {
        void AdjustPrice(double adjustment);
        double GetFinalPrice();
        double GetAdjustment();
        Order Order { get; set; }
    }
}