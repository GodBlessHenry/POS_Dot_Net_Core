using POS.Core.Service;

namespace POS.Core.Decorator
{
    public class ShoppingCart : IFinalPrice
    {
        private double Adjustment { get; set; }
        private double FinalPrice { get; set; }
        public Order Order { get; set; }

        public ShoppingCart(IOrderRepository orderRepository)
        {
            Order = orderRepository.GetById();
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