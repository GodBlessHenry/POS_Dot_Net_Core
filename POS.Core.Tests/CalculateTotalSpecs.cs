using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using POS.Core.Services;
using POS.Core.Tests.Helpers;

namespace POS.Core.Tests
{
    [TestClass]
    public class CalculateTotalSpecs
    {
        [TestClass]
        public class WhenCalculatingTotal : SpecsBase<CalculateTotal>
        {
            private decimal _result;
            private decimal _expectedResult;
            private const decimal DiscountAmount = 0.9m;
            private const int Id = 1;

            protected override void Given()
            {
                Mock<IDiscount>().Get().Returns(DiscountAmount);
                _expectedResult = Id * DiscountAmount;
            }

            protected override void When()
            {
                _result = Sut.Calculate(Id);
            }

            [TestMethod]
            public void ThenShouldCallGetDiscount()
            {
                Mock<IDiscount>().Received(1).Get();
            }


            [TestMethod]
            public void ThenShouldReturnCorrectResult()
            {
                _result.Should().Be(_expectedResult);
            }
        }
    }
}
