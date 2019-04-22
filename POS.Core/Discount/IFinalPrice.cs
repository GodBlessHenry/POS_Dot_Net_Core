namespace POS.Core.Discount
{
    // This is the interface for the concrete class 
    public interface IFinalPrice
    {
        void AdjustPrice(double adjustment);
        double GetFinalPrice();
        double GetAdjustment();
        Order Order { get; set; }
    }
}