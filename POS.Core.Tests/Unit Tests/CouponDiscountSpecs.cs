using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using POS.Core.Discount;
using POS.Core.Service;
using POS.Core.Tests.Builders;
using POS.Core.Tests.Helpers;

namespace POS.Core.Tests.UnitTests
{
    [TestClass]
    public class CouponDiscountSpecs
    {
        private const double OffAmt = 5d;
        private const double EligibleAmt = 100d;

        [TestClass]
        public class WhenQualifyCouponDiscount : SpecsBase<CouponDiscount>
        {
            private Order _order;
            private const bool WithCoupon = true;
            private const double TotalPriceBeforeCoupon = 105d;
            private double _expectedAdjustment;

            protected override void Given()
            {
                _order = Build.Order().WithCoupon(WithCoupon); 
                Mock<IFinalPrice>().Order.Returns(_order);
                Mock<IFinalPrice>().GetFinalPrice().Returns(TotalPriceBeforeCoupon);
                Mock<IDiscountVariablesRepository>().EligibleAmt.Returns(EligibleAmt);
                Mock<IDiscountVariablesRepository>().OffAmt.Returns(OffAmt);
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
            private const double TotalPriceBeforeCoupon = 105d;
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