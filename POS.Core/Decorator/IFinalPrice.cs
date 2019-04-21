namespace POS.Core.Decorator
{
    public interface IFinalPrice
    {
        void AdjustPrice(double adjustment);
        double GetFinalPrice();
        Order Order { get; set; }
    }
}