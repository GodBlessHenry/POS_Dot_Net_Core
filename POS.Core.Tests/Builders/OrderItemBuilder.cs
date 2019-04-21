namespace POS.Core.Tests.Builders
{
    public class OrderItemBuilder : Builder<OrderItem>
    {
        public OrderItemBuilder()
        {
            Creation = new OrderItem
            {
                Product = Build.Product(),
                Unit = ProductUnit.Box,
                Quantity = Build.RandomInt()
            };
        }

        public OrderItemBuilder WithQuantity(int quantity)
        {
            Creation.Quantity = quantity;
            return this;
        }

        public OrderItemBuilder WithUnit(ProductUnit unit)
        {
            Creation.Unit = unit;
            return this;
        }

        public OrderItemBuilder WithProduct(Product product)
        {
            Creation.Product = product;
            return this;
        }
    }
}