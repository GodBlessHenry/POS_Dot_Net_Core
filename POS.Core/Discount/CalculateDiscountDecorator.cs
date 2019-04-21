namespace POS.Core.Services
{
    public class CalculateDiscountDecorator : ICalculateDiscount 
    {
        protected ICalculateDiscount BaseDiscountCalculator;

        public void SetBaseDiscount(ICalculateDiscount calculateDiscount)
        {
            BaseDiscountCalculator = calculateDiscount;
        }

        public virtual double CalculateDiscountPrice()
        {
            return BaseDiscountCalculator?.CalculateDiscountPrice() ?? 0d;
        }
    }
}