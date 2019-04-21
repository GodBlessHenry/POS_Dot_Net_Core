using System.Linq;
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
    public class CalculateOrder_NoDiscountSpecs
    {
        [TestClass]
        public class WhenCalculatingTotal : SpecsBase<CalculateOrder_NoDiscount>
        {
            private Order _order;
            private double _expectedResult;
            private double _actionResult;

            protected override void Given()
            {
                _order = Build.Order();
                _expectedResult = _order.OrderItems.Sum(x => x.Product.Price * x.Quantity);
                Mock<IOrderRepository>().GetById().Returns(_order);
            }

            protected override void When()
            {
                _actionResult = Sut.CalculateDiscountPrice();
            }

            [TestMethod]
            public void ShoulGetCorrectPrice()
            {
                _actionResult.Should().Be(_expectedResult);
            }
        }
    }
}