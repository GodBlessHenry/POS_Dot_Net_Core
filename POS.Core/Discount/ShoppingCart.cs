using POS.Core.Service;

namespace POS.Core.Discount
{
    // This is the concrete class of the interface
    public class ShoppingCart : IFinalPrice
    {
        public double Adjustment { get; protected set; }
        public double FinalPrice { get; protected set; }
        private readonly IOrderRepository _orderRepository;
        public Order Order { get; set; }

        public ShoppingCart(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void GetOrder(int id)
        {
            Order = _orderRepository.GetById(id);
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