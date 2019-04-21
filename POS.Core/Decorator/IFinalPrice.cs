namespace POS.Core.Decorator
{
    public interface IFinalPrice
    {
        //double Adjustment { get; set; }
        void AdjustPrice(double adjustment);
        double GetFinalPrice();
        Order Order { get; set; }
    }
}