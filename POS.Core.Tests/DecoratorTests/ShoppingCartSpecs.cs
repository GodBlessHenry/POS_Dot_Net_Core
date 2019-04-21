using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS.Core.Decorator;
using POS.Core.Tests.Helpers;

namespace POS.Core.Tests.DecoratorTests
{
    [TestClass]
    public class ShoppingCartSpecs
    {
        [TestClass]
        public class WhenAdjustPrice : SpecsBase<ShoppingCart>
        {
            private double _adjustment;

            protected override void Given()
            {
                _adjustment = 5d;
            }

            protected override void When()
            {
                Sut.AdjustPrice(_adjustment);
            }

            [TestMethod]
            public void ThenShouldUpdateAdjustmentWithCorrectNumber()
            {
                Sut.Adjustment.Should().Be(_adjustment);
            }

            [TestMethod]
            public void ThenShouldUpdatePriceWithCorrectNumber()
            {
                Sut.FinalPrice.Should().Be(-1 * _adjustment);
            }
        }
    }
}