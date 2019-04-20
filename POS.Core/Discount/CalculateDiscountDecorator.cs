namespace POS.Core.Services
{
    public class CalculateDiscountDecorator : CalculateDiscountBase
    {
        //protected decimal Total;
        protected CalculateDiscountBase CalculateDiscount;

        public void SetDiscount(CalculateDiscountBase calculateDiscount)
        {
            CalculateDiscount = calculateDiscount;
        }

        public override decimal CalculateDiscountPrice()
        {
            return CalculateDiscount?.CalculateDiscountPrice() ?? 0m;
        }
    }
}