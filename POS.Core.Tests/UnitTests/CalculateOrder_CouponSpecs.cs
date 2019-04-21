using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using POS.Core.Service;
using POS.Core.Services;
using POS.Core.Tests.Builders;
using POS.Core.Tests.Helpers;

namespace POS.Core.Tests
{
    [TestClass]
    public class CalculateOrder_CouponSpecs
    {
        [TestClass]
        public class WhenSubTotalQualifyForCouponDiscount : SpecsBase<CalculateOrder_Coupon>
        {
            private decimal _actualResult;
            private const int BulkQty = 2;
            private const int FreeQty = 1;
            private const decimal EligibleAmt = 100m;
            private const decimal OffAmt = 5m;
            private const int Quantity = 2;
            private const decimal TotalAfterBaseDiscount = 105;
            private Order _order;
            private decimal _expectedResult;

            protected override void Given()
            {
                Mock<IDiscountVariables>().FreeQty.Returns(FreeQty);
                Mock<IDiscountVariables>().BulkQty.Returns(BulkQty);
                Mock<IDiscountVariables>().EligibleAmt.Returns(EligibleAmt);
                Mock<IDiscountVariables>().OffAmt.Returns(OffAmt);

                _order = Build.Order().WithCoupon(true).WithQuantity(Quantity).WithCanUseBulkDiscount(true);
                
                Mock<IOrderRepository>().GetById().Returns(_order);
                Mock<ICalculateDiscount>().CalculateDiscountPrice().Returns(TotalAfterBaseDiscount);
                _expectedResult = TotalAfterBaseDiscount - OffAmt;
            }

            protected override void When()
            {
                Sut.SetBaseDiscount(Mock<ICalculateDiscount>());
                _actualResult = Sut.CalculateDiscountPrice();
            }

            [TestMethod]
            public void ThenShouldCallBulkDiscountCalculator()
            {
                Mock<ICalculateDiscount>().Received(1).CalculateDiscountPrice();
            }

            [TestMethod]
            public void ThenShouldReturnCorrectPrice()
            {
                _actualResult.Should().Be(_expectedResult);
            }
        }

        [TestClass]
        public class WhenSubTotalQualifyForCouponDiscountButNoCoupon : SpecsBase<CalculateOrder_Coupon>
        {
            private decimal _actualResult;
            private const int BulkQty = 2;
            private const int FreeQty = 1;
            private const decimal EligibleAmt = 100m;
            private const decimal OffAmt = 5m;
            private const int Quantity = 2;
            private const decimal TotalAfterBaseDiscount = 105;
            private Order _order;
            private decimal _expectedResult;

            protected override void Given()
            {
                Mock<IDiscountVariables>().FreeQty.Returns(FreeQty);
                Mock<IDiscountVariables>().BulkQty.Returns(BulkQty);
                Mock<IDiscountVariables>().EligibleAmt.Returns(EligibleAmt);
                Mock<IDiscountVariables>().OffAmt.Returns(OffAmt);

                _order = Build.Order().WithCoupon(false).WithQuantity(Quantity).WithCanUseBulkDiscount(true);

                Mock<IOrderRepository>().GetById().Returns(_order);
                Mock<ICalculateDiscount>().CalculateDiscountPrice().Returns(TotalAfterBaseDiscount);
                _expectedResult = TotalAfterBaseDiscount;
            }

            protected override void When()
            {
                Sut.SetBaseDiscount(Mock<ICalculateDiscount>());
                _actualResult = Sut.CalculateDiscountPrice();
            }

            [TestMethod]
            public void ThenShouldCallBulkDiscountCalculator()
            {
                Mock<ICalculateDiscount>().Received(1).CalculateDiscountPrice();
            }

            [TestMethod]
            public void ThenShouldReturnCorrectPrice()
            {
                _actualResult.Should().Be(_expectedResult);
            }
        }

        [TestClass]
        public class WhenSubTotalNotQualifyForCouponDiscount : SpecsBase<CalculateOrder_Coupon>
        {
            private decimal _actualResult;
            private const int BulkQty = 2;
            private const int FreeQty = 1;
            private const decimal EligibleAmt = 100m;
            private const decimal OffAmt = 5m;
            private const int Quantity = 2;
            private const decimal TotalAfterBaseDiscount = 95;
            private Order _order;
            private decimal _expectedResult;

            protected override void Given()
            {
                Mock<IDiscountVariables>().FreeQty.Returns(FreeQty);
                Mock<IDiscountVariables>().BulkQty.Returns(BulkQty);
                Mock<IDiscountVariables>().EligibleAmt.Returns(EligibleAmt);
                Mock<IDiscountVariables>().OffAmt.Returns(OffAmt);

                _order = Build.Order().WithCoupon(true).WithQuantity(Quantity).WithCanUseBulkDiscount(true);

                Mock<IOrderRepository>().GetById().Returns(_order);
                Mock<ICalculateDiscount>().CalculateDiscountPrice().Returns(TotalAfterBaseDiscount);
                _expectedResult = TotalAfterBaseDiscount;
            }

            protected override void When()
            {
                Sut.SetBaseDiscount(Mock<ICalculateDiscount>());
                _actualResult = Sut.CalculateDiscountPrice();
            }

            [TestMethod]
            public void ThenShouldCallBulkDiscountCalculator()
            {
                Mock<ICalculateDiscount>().Received(1).CalculateDiscountPrice();
            }

            [TestMethod]
            public void ThenShouldReturnCorrectPrice()
            {
                _actualResult.Should().Be(_expectedResult);
            }
        }
    }
}