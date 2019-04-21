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



        // GET api/OrderTotals/FinalPrice
        [HttpGet]
        [Route("FinalPrice")]
        public IActionResult GetFinalPrice()
        {
            var message = new CalculateOrderTotal(_discountVariables, _orderRepository).CalculateFinalPrice();

            if (string.IsNullOrEmpty(message))
            {
                return NotFound();
            }

            return Ok(message);
        }

        // GET api/OrderTotals/BulkAndCouponDiscount
        [HttpGet]
        [Route("BulkAndCouponDiscount")]
        public IActionResult GetTotalWithBulkAndCouponDiscount()
        {
            var total = new CalculateOrderTotal(_discountVariables, _orderRepository).CalculateWithBulkAndCouponDiscount();

            if (total <= 0)
            {
                return NotFound();
            }

            return Ok(total);
        }

        // GET api/OrderTotals/CouponDiscount
        [HttpGet]
        [Route("CouponDiscount")]
        public IActionResult GetTotalWithCouponeDiscount()
        {
            var total = new CalculateOrderTotal(_discountVariables, _orderRepository).CalculateWithCouponDiscount();

            if (total <= 0)
            {
                return NotFound();
            }

            return Ok(total);
        }

        // GET api/OrderTotals/BulkDiscount
        [HttpGet]
        [Route("BulkDiscount")]
        public IActionResult GetTotalWithBulkDiscount()
        {
            var total = new CalculateOrderTotal(_discountVariables, _orderRepository).CalculateWithBulkDiscount();

            if (total <= 0)
            {
                return NotFound();
            }

            return Ok(total);
        }

        // GET api/OrderTotals/NoDiscount
        [HttpGet]
        [Route("NoDiscount")]
        public IActionResult GetTotalWithNoDiscount()
        {
            //var total = new CalculateOrderTotal(_discountVariables, _orderRepository).CalculateWithNoDiscount();
            var bulk = new CalculateOrder_NoDiscount(_orderRepository);

            var total = bulk.CalculateDiscountPrice();

            if (total <= 0)
            {
                return NotFound();
            }

            return Ok(total);
        }

    }
}