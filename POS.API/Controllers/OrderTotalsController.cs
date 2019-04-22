using Microsoft.AspNetCore.Mvc;
using POS.Core.Service;
using POS.Core.Services;

namespace POS.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderTotalsController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDiscountVariables _discountVariables;

        public OrderTotalsController(IOrderRepository orderRepository, IDiscountVariables discountVariables)
        {
            _orderRepository = orderRepository;
            _discountVariables = discountVariables;
        }

        // GET api/OrderTotals/orderId/FinalPrice
        [HttpGet]
        [Route("{orderId:int}/FinalPrice")]
        public IActionResult GetFinalPrice(int orderId)
        {
            var message = new ShoppingCartCalculator(_discountVariables, _orderRepository)
                                .CalculateFinalPrice(orderId);

            if (string.IsNullOrEmpty(message))
            {
                return NotFound();
            }

            return Ok(message);
        }
    }
}