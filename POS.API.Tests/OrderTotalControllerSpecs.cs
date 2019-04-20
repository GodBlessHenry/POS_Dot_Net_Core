using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using POS.API.Controllers;
using POS.Core.Services;
using POS.Core.Tests.Helpers;
    
namespace POS.API.Tests
{
    [TestClass]
    public class OrderTotalControllerSpecs
    {
        [TestClass]
        public class WhenGettingTotal : SpecsBase<OrderTotalsController>
        {
            private IActionResult _result;
            private const decimal Total = 0.9m;
            private const int Id = 1;

            protected override void Given()
            {
                Mock<ICalculateTotal>().Calculate(Id).Returns(Total);
            }

            protected override void When()
            {
                _result = Sut.Get(Id) as ActionResult;
            }

            [TestMethod]
            public void ShouldReturnHttpOkStatus()
            {
                _result.Should().NotBeNull();
                _result.Should().BeOfType(typeof(OkObjectResult));
            }

            [TestMethod]
            public void ShouldReturnCorrectTotal()
            {
                var response = _result as OkObjectResult;
                response.Value.Should().Be(Total);
            }
        }
    }
}
