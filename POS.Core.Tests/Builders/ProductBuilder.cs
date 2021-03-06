﻿namespace POS.Core.Tests.Builders
{
    public class ProductBuilder : Builder<Product>
    {
        public ProductBuilder()
        {
            Creation = new Product
            {
                Name = Build.RandomString(),
                Price = Build.RandomDouble(),
                CanUseBulkDiscount = true
            };
        }

        public ProductBuilder WithCanUseBulkDiscount(bool canUseBulkDiscount)
        {
            Creation.CanUseBulkDiscount = canUseBulkDiscount;
            return this;
        }
        public ProductBuilder WithPrice(double price)
        {
            Creation.Price = price;
            return this;
        }
    }
}