using POS.Core.Service;

namespace POS.Core.Decorator
{
    public class ShoppingCart : IFinalPrice
    {
        public double Adjustment { get; protected set; }
        public double FinalPrice { get; protected set; }
        public Order Order { get; set; }

        public ShoppingCart()
        {
        }

        public void AdjustPrice(double adjustment)
        {
            FinalPrice -= adjustment;
            Adjustment = adjustment;
        }

        public double GetFinalPrice()
        {
            return FinalPrice;
        }

        public double GetAdjustment()
        {
            return Adjustment;
        }
    }
}