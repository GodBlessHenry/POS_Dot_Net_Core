using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using POS.Core.Decorator;
using POS.Core.Tests.Builders;
using POS.Core.Tests.Helpers;

namespace POS.Core.Tests.DecoratorTests
{
    [TestClass]
    public class CouponDiscountSpecs
    {
        [TestClass]
        public class WhenQualifyCouponDiscount : SpecsBase<CouponDiscount>
        {
            private Order _order;
            private const bool WithCoupon = true;
            private const double TotalPriceBeforeCoupon = 100d;
            private const double OffAmt = 5d;
            private double _expectedAdjustment;

            protected override void Given()
            {
                _order = Build.Order().WithCoupon(WithCoupon); 
                Mock<IFinalPrice>().Order.Returns(_order);
                Mock<IFinalPrice>().GetFinalPrice().Returns(TotalPriceBeforeCoupon);

                _expectedAdjustment = OffAmt;
            }

            protected override void When()
            {
                Sut.AdjustPriceByCouponDiscount();
            }

            [TestMethod]
            public void ThenShouldAdjustPriceByCouponDiscountWithCorrectAdjustment()
            {
                Mock<IFinalPrice>().Received(1).AdjustPrice(_expectedAdjustment);
            }
        }

        [TestClass]
        public class WhenTotalPriceNotQualifyCouponDiscount : SpecsBase<CouponDiscount>
        {
            private Order _order;
            private const bool WithCoupon = true;
            private const double TotalPriceBeforeCoupon = 99d;
            private const double NoDiscount = 0d;
            private double _expectedAdjustment;

            protected override void Given()
            {
                _order = Build.Order().WithCoupon(WithCoupon);
                Mock<IFinalPrice>().Order.Returns(_order);
                Mock<IFinalPrice>().GetFinalPrice().Returns(TotalPriceBeforeCoupon);

                _expectedAdjustment = NoDiscount;
            }

            protected override void When()
            {
                Sut.AdjustPriceByCouponDiscount();
            }

            [TestMethod]
            public void ThenShouldAdjustPriceByCouponDiscountWithCorrectAdjustment()
            {
                Mock<IFinalPrice>().Received(1).AdjustPrice(_expectedAdjustment);
            }
        }

        [TestClass]
        public class WhenTotalPriceQualifyCouponDiscountButNoCoupon : SpecsBase<CouponDiscount>
        {
            private Order _order;
            private const bool WithCoupon = false;
            private const double TotalPriceBeforeCoupon = 100d;
            private const double NoDiscount = 0d;
            private double _expectedAdjustment;

            protected override void Given()
            {
                _order = Build.Order().WithCoupon(WithCoupon);
                Mock<IFinalPrice>().Order.Returns(_order);
                Mock<IFinalPrice>().GetFinalPrice().Returns(TotalPriceBeforeCoupon);

                _expectedAdjustment = NoDiscount;
            }

            protected override void When()
            {
                Sut.AdjustPriceByCouponDiscount();
            }

            [TestMethod]
            public void ThenShouldAdjustPriceByCouponDiscountWithCorrectAdjustment()
            {
                Mock<IFinalPrice>().Received(1).AdjustPrice(_expectedAdjustment);
            }
        }
    }
}