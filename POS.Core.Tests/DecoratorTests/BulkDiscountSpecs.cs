using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using POS.Core.Decorator;
using POS.Core.Tests.Builders;
using POS.Core.Tests.Helpers;

namespace POS.Core.Tests.DecoratorTests
{
    [TestClass]
    public class BulkDiscountSpecs
    {
        [TestClass]
        public class WhenQualifyBulkDiscount : SpecsBase<BulkDiscount>
        {
            private Order _order;
            private ProductBuilder _product;
            private OrderItemBuilder _orderItem;
            private const bool CanUseBulkDiscount = true;
            private const bool WithCoupon = true;
            private const int Quantity = 10;
            private const double Price = 4;
            private const int BulkQty = 2;
            private const int FreeQty = 1;
            public const int BulkAndFree = BulkQty + FreeQty;
            private double _expectedAdjustment;

            protected override void Given()
            {
                _product = Build.Product().WithCanUseBulkDiscount(CanUseBulkDiscount).WithPrice(Price);
                _orderItem = Build.OrderItem().WithProduct(_product).WithQuantity(Quantity);
                _order = Build.Order().WithCoupon(WithCoupon).WithOrderItem(new List<OrderItem> { _orderItem });
                _expectedAdjustment = Price * (Quantity - Quantity % BulkAndFree) / BulkAndFree ;

                Mock<IFinalPrice>().Order.Returns(_order);
            }

            protected override void When()
            {
                Sut.AdjustPriceByBulkDiscount();
            }

            [TestMethod]
            public void ThenShouldAdjustPriceByBulkDiscount()
            {
                Mock<IFinalPrice>().Received(1).AdjustPrice(_expectedAdjustment);
            }
        }

        [TestClass]
        public class WhenQualifyBulkDiscountButCannotUseBulkDiscount : SpecsBase<BulkDiscount>
        {
            private Order _order;
            private ProductBuilder _product;
            private OrderItemBuilder _orderItem;
            private const bool CanUseBulkDiscount = false;
            private const bool WithCoupon = true;
            private const int Quantity = 10;
            private const double Price = 4;
            private const double NoDiscount = 0d;
            private double _expectedAdjustment;

            protected override void Given()
            {
                _product = Build.Product().WithCanUseBulkDiscount(CanUseBulkDiscount).WithPrice(Price);
                _orderItem = Build.OrderItem().WithProduct(_product).WithQuantity(Quantity);
                _order = Build.Order().WithCoupon(WithCoupon).WithOrderItem(new List<OrderItem> { _orderItem });
                _expectedAdjustment = NoDiscount;

                Mock<IFinalPrice>().Order.Returns(_order);
            }

            protected override void When()
            {
                Sut.AdjustPriceByBulkDiscount();
            }

            [TestMethod]
            public void ThenShouldAdjustPriceByBulkDiscount()
            {
                Mock<IFinalPrice>().Received(1).AdjustPrice(_expectedAdjustment);
            }
        }

        [TestClass]
        public class WhenCanUseBulkDiscountButNotEnoughQuantity : SpecsBase<BulkDiscount>
        {
            private Order _order;
            private ProductBuilder _product;
            private OrderItemBuilder _orderItem;
            private const bool CanUseBulkDiscount = true;
            private const bool WithCoupon = true;
            private const int Quantity = 2;
            private const double Price = 4;
            private const double NoDiscount = 0d;
            private double _expectedAdjustment;

            protected override void Given()
            {
                _product = Build.Product().WithCanUseBulkDiscount(CanUseBulkDiscount).WithPrice(Price);
                _orderItem = Build.OrderItem().WithProduct(_product).WithQuantity(Quantity);
                _order = Build.Order().WithCoupon(WithCoupon).WithOrderItem(new List<OrderItem> { _orderItem });
                _expectedAdjustment = NoDiscount;

                Mock<IFinalPrice>().Order.Returns(_order);
            }

            protected override void When()
            {
                Sut.AdjustPriceByBulkDiscount();
            }

            [TestMethod]
            public void ThenShouldAdjustPriceByBulkDiscount()
            {
                Mock<IFinalPrice>().Received(1).AdjustPrice(_expectedAdjustment);
            }
        }
    }
}