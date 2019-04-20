namespace POS.Core.Services
{
    public class CalculateTotal : ICalculateTotal
    {
        private readonly IDiscount _discount;

        public CalculateTotal(IDiscount discount)
        {
            _discount = discount;
        }

        public decimal Calculate(int id)
        {
            return (decimal)(id * _discount.Get());
        }
    }
}