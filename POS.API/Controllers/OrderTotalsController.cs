using Microsoft.AspNetCore.Mvc;
using POS.Core;
using POS.Core.Service;

namespace POS.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderTotalsController : Controller
    {
        private readonly IShoppingCartCalculator _shoppingCartCalculator;
        

        public OrderTotalsController(IShoppingCartCalculator shoppingCartCalculator)
        {
            _shoppingCartCalculator = shoppingCartCalculator;
        }

        // GET api/OrderTotals/orderId/FinalPrice
        [HttpGet]
        [Route("{orderId:int}/FinalPrice")]
        public IActionResult GetFinalPrice(int orderId)
        {
            var message = _shoppingCartCalculator.CalculateFinalPrice(orderId);

            if (string.IsNullOrEmpty(message))
            {
                return NotFound();
            }

            return Ok(message);
        }
    }
}