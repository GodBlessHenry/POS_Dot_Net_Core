using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using POS.Core;
using POS.Core.Services;

namespace POS.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderTotalsController : Controller
    {
        private readonly Order _order;

        //private readonly ICalculateTotal _calculateTotal;

        //public OrderTotalsController(ICalculateTotal calculateTotal)
        //{
        //    _calculateTotal = calculateTotal;
        //}

        public OrderTotalsController()
        {
            _order = new Order();

            _order.OrderItems = new List<OrderItem>();
            _order.OrderItems.Add(new OrderItem { Product = new Product { Name = "Cheerio", Price = 6.99m }, Unit = ProductUnit.Box, Quantity = 4 });
            _order.WithCoupon = true;

            //{
            //    OrderItems = {
            //    new OrderItem { Product = new Product{Name = "Cheerio", Price = 6.99m }, Unit = ProductUnit.Box, Quantity = 4 },
            //    new OrderItem { Product = new Product{Name = "Apple", Price = 2.49m }, Unit = ProductUnit.Pound, Quantity = 10 }
            //    },
            //    WithCoupon = true
            //};

            //_order = new Order
            //{
            //    OrderItems = {
            //    new OrderItem { Product = new Product{Name = "Cheerio", Price = 6.99m }, Unit = ProductUnit.Box, Quantity = 4 },
            //    new OrderItem { Product = new Product{Name = "Apple", Price = 2.49m }, Unit = ProductUnit.Pound, Quantity = 10 }
            //},
            //    WithCoupon = true
            //};
        }




        // GET api/OrderTotals/BulkAndCouponDiscount
        [HttpGet]
        [Route("BulkAndCouponDiscount")]
        public IActionResult GetTotalWithBulkAndCoupon()
        {
            var total = new CalculateOrderTotal(_order).CalculateWithBulkAndCouponDiscount();


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
            var total = new CalculateOrderTotal(_order).CalculateWithCouponDiscount();

            if (total <= 0)
            {
                return NotFound();
            }

            return Ok(total);
        }
    }
}