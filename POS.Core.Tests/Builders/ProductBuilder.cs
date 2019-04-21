namespace POS.Core.Tests.Builders
{
    public class ProductBuilder : Builder<Product>
    {
        public ProductBuilder()
        {
            Creation = new Product
            {
                Name = Build.RandomString(),
                Price = Build.Randomdouble(),
                CanUseBulkDiscount = true
            };
        }

        public ProductBuilder WithCanUseBulkDiscount(bool canUseBulkDiscount)
        {
            Creation.CanUseBulkDiscount = canUseBulkDiscount;
            return this;
        }
    }
}