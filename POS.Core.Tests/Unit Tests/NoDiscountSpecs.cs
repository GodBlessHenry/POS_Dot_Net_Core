using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using POS.Core.Discount;
using POS.Core.Tests.Builders;
using POS.Core.Tests.Helpers;

namespace POS.Core.Tests.DecoratorTests
{
    [TestClass]
    public class NoDiscountSpecs
    {
        [TestClass]
        public class WhenCalculatePrice : SpecsBase<NoDiscount>
        {
            private Order _order;
            private ProductBuilder _product;
            private OrderItemBuilder _orderItem;
            private const double Price = 2;
            private const bool CanUseBulkDiscount = true;
            private const bool WithCoupon = true;
            private const int Quantity = 10;

            private double _expectedAdjustment;

            protected override void Given()
            {
                _product = Build.Product().WithCanUseBulkDiscount(CanUseBulkDiscount).WithPrice(Price);
                _orderItem = Build.OrderItem().WithProduct(_product).WithQuantity(Quantity);
                _order = Build.Order().WithCoupon(WithCoupon).WithOrderItem(new List<OrderItem> {_orderItem});
                _expectedAdjustment = -1 * Price * Quantity;

                Mock<IFinalPrice>().Order.Returns(_order);
            }

            protected override void When()
            {
                Sut.CalculatePriceWithoutDiscount();
            }

            [TestMethod]
            public void ThenShouldCallAdjustPriceWIthCorrectAdjustment()
            {
                Mock<IFinalPrice>().Received(1).AdjustPrice(_expectedAdjustment);
            }
        }
    }
}