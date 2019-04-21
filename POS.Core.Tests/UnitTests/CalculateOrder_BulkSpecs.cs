using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using POS.Core.Service;
using POS.Core.Services;
using POS.Core.Tests.Builders;
using POS.Core.Tests.Helpers;

namespace POS.Core.Tests
{
    [TestClass]
    public class CalculateOrder_BulkSpecs
    {
        [TestClass]
        public class WhenQuantityQualifyBulkDiscount : SpecsBase<CalculateOrder_Bulk>
        {
            private Order _order;
            private decimal _expectedResult;
            private decimal _actualResult;
            private const int BulkQty = 2;
            private const int FreeQty = 1;
            private const decimal EligibleAmt = 100m;
            private const decimal OffAmt = 5m;
            private const int Quantity = 10;
            private const int BulkAndFree = BulkQty + FreeQty;

            protected override void Given()
            {
                Mock<IDiscountVariables>().FreeQty.Returns(FreeQty);
                Mock<IDiscountVariables>().BulkQty.Returns(BulkQty);
                Mock<IDiscountVariables>().EligibleAmt.Returns(EligibleAmt);
                Mock<IDiscountVariables>().OffAmt.Returns(OffAmt);
                _order = Build.Order().WithCoupon(true).WithQuantity(Quantity).WithCanUseBulkDiscount(true);
                Mock<IOrderRepository>().GetById().Returns(_order);

                _expectedResult = (Quantity -(Quantity - Quantity % BulkAndFree) / BulkAndFree) *
                                  _order.OrderItems[0].Product.Price;
            }

            protected override void When()
            {
                _actualResult = Sut.CalculateDiscountPrice();
            }

            [TestMethod]
            public void ThenShouldCallOrderRepository()
            {
                Mock<IOrderRepository>().Received(1).GetById();
            }


            [TestMethod]
            public void ThenShouldReturnCorrectResult()
            {
                _actualResult.Should().Be(_expectedResult);
            }
        }

        [TestClass]
        public class WhenQuantityQualifyBulkDiscountButSaleIsNotOn : SpecsBase<CalculateOrder_Bulk>
        {
            private Order _order;
            private decimal _expectedResult;
            private decimal _actualResult;
            private const int BulkQty = 2;
            private const int FreeQty = 1;
            private const decimal EligibleAmt = 100m;
            private const decimal OffAmt = 5m;
            private const int Quantity = 10;

            protected override void Given()
            {
                Mock<IDiscountVariables>().FreeQty.Returns(FreeQty);
                Mock<IDiscountVariables>().BulkQty.Returns(BulkQty);
                Mock<IDiscountVariables>().EligibleAmt.Returns(EligibleAmt);
                Mock<IDiscountVariables>().OffAmt.Returns(OffAmt);
                _order = Build.Order().WithCanUseBulkDiscount(false).WithCoupon(true).WithQuantity(Quantity);
                Mock<IOrderRepository>().GetById().Returns(_order);

                _expectedResult = Quantity * _order.OrderItems[0].Product.Price;
            }

            protected override void When()
            {
                _actualResult = Sut.CalculateDiscountPrice();
            }

            [TestMethod]
            public void ThenShouldCallOrderRepository()
            {
                Mock<IOrderRepository>().Received(1).GetById();
            }


            [TestMethod]
            public void ThenShouldReturnCorrectResult()
            {
                _actualResult.Should().Be(_expectedResult);
            }
        }

        [TestClass]
        public class WhenQuantityNotQualifyBulkDiscount : SpecsBase<CalculateOrder_Bulk>
        {
            private Order _order;
            private decimal _expectedResult;
            private decimal _actualResult;
            private const int BulkQty = 2;
            private const int FreeQty = 1;
            private const decimal EligibleAmt = 100m;
            private const decimal OffAmt = 5m;
            private const int Quantity = 2;

            protected override void Given()
            {
                Mock<IDiscountVariables>().FreeQty.Returns(FreeQty);
                Mock<IDiscountVariables>().BulkQty.Returns(BulkQty);
                Mock<IDiscountVariables>().EligibleAmt.Returns(EligibleAmt);
                Mock<IDiscountVariables>().OffAmt.Returns(OffAmt);
                _order = Build.Order().WithCoupon(true).WithQuantity(Quantity).WithCanUseBulkDiscount(true);
                Mock<IOrderRepository>().GetById().Returns(_order);

                _expectedResult = Quantity * _order.OrderItems[0].Product.Price;
            }

            protected override void When()
            {
                _actualResult = Sut.CalculateDiscountPrice();
            }

            [TestMethod]
            public void ThenShouldCallOrderRepository()
            {
                Mock<IOrderRepository>().Received(1).GetById();
            }


            [TestMethod]
            public void ThenShouldReturnCorrectResult()
            {
                _actualResult.Should().Be(_expectedResult);
            }
        }
    }
}