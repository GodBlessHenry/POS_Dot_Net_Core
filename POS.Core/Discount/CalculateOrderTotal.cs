﻿using System.Runtime.InteropServices;
using POS.Core.Decorator;
using POS.Core.Service;

namespace POS.Core.Services
{
    public class CalculateOrderTotal
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDiscountVariables _discountVariables;

        public CalculateOrderTotal(IDiscountVariables discountVariables, IOrderRepository orderRepository)
        {
            _discountVariables = discountVariables;
            _orderRepository = orderRepository;
        }


        #region Decorator patter

        public string CalculateFinalPrice()
        {
            var view = "";
            var cart = new ShoppingCart(_orderRepository);
            var noDiscount = new NoDiscount(cart);
            noDiscount.CalculatePriceWithoutDiscount();
            view += "Price without any discount  : $" + cart.GetFinalPrice() + " \n";

            var bulkDiscount = new BulkDiscount(noDiscount);
            bulkDiscount.AdjustPriceByBulkDiscount();
            view += "Bulk discount adjustment    : $" + cart.GetAdjustment() * -1 + " \n";
            view += "Price after bulk discount   : $" + cart.GetFinalPrice() + " \n";

            var coupon = new CouponDiscount(bulkDiscount);
            coupon.AdjustPriceByCouponDiscount();
            view += "Coupon discount adjustment  : $" + cart.GetAdjustment() * -1 + " \n";
            view += "Price after coupon discount : $" + cart.GetFinalPrice() + " \n";

            return view;
        }

        #endregion




        public double CalculateWithNoDiscount()
        {
            var bulk = new CalculateOrder_NoDiscount(_orderRepository);

            return bulk.CalculateDiscountPrice();
        }

        public double CalculateWithBulkDiscount()
        {
            var bulk = new CalculateOrder_Bulk(_discountVariables, _orderRepository);

            return bulk.CalculateDiscountPrice();
        }

        public double CalculateWithBulkAndCouponDiscount()
        {
            var coupon = new CalculateOrder_Coupon(_discountVariables, _orderRepository);
            coupon.SetBaseDiscount(new CalculateOrder_Bulk(_discountVariables, _orderRepository));

            return coupon.CalculateDiscountPrice();
        }

        public double CalculateWithCouponDiscount()
        {
            var coupon = new CalculateOrder_Coupon(_discountVariables, _orderRepository);
            coupon.SetBaseDiscount(new CalculateOrder_NoDiscount(_orderRepository));

            return coupon.CalculateDiscountPrice();
        }

    }
}