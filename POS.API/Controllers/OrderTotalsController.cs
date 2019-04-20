using Microsoft.AspNetCore.Mvc;
using POS.Core.Services;

namespace POS.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderTotalsController : Controller
    {
        private readonly ICalculateTotal _calculateTotal;

        public OrderTotalsController(ICalculateTotal calculateTotal)
        {
            _calculateTotal = calculateTotal;
        }

        // GET api/OrderTotals/5
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var total = _calculateTotal.Calculate(id);

            if (total <= 0)
            {
                return NotFound();
            }

            return Ok(total);
        }
    }
}