namespace POS.Core.Services
{
    public class CalculateDiscountDecorator : ICalculateDiscount 
    {
        protected ICalculateDiscount BaseDiscountCalculator;

        public void SetBaseDiscount(ICalculateDiscount calculateDiscount)
        {
            BaseDiscountCalculator = calculateDiscount;
        }

        public virtual decimal CalculateDiscountPrice()
        {
            return BaseDiscountCalculator?.CalculateDiscountPrice() ?? 0m;
        }
    }
}