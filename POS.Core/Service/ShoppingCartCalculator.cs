using POS.Core.Discount;
using POS.Core.Service;

namespace POS.Core.Services
{
    public class ShoppingCartCalculator
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDiscountVariables _discountVariables;

        public ShoppingCartCalculator(IDiscountVariables discountVariables, IOrderRepository orderRepository)
        {
            _discountVariables = discountVariables;
            _orderRepository = orderRepository;
        }

        public string CalculateFinalPrice(int orderId)
        {
            var cart = InitiateShoppingCart(orderId, out var view);
            var noDiscount = UseNoDiscount(cart, ref view);
            var bulkDiscount = UseBulkDiscount(noDiscount, ref view);
            UseCouponDiscount(bulkDiscount, ref view);

            return view;
        }

#region Discount 

        private IFinalPrice InitiateShoppingCart(int orderId, out string view)
        {
            var cart = new ShoppingCart(_orderRepository);
            cart.GetOrder(orderId);
            view = "";
            return cart;
        }

        private IFinalPrice UseNoDiscount(IFinalPrice cart, ref string view)
        {
            var noDiscount = new NoDiscount(cart);
            noDiscount.CalculatePriceWithoutDiscount();
            view += "Price without any discount  : $" + cart.GetFinalPrice() + " \n";

            return noDiscount.AdjustedCart;
        }

        private IFinalPrice UseBulkDiscount(IFinalPrice cart, ref string view)
        {
            var bulkDiscount = new BulkDiscount(cart, _discountVariables);
            bulkDiscount.AdjustPriceByBulkDiscount();
            view += "Bulk discount adjustment    : $" + cart.GetAdjustment() * -1 + " \n";
            view += "Price after bulk discount   : $" + cart.GetFinalPrice() + " \n";

            return bulkDiscount.AdjustedCart;
        }

        private IFinalPrice UseCouponDiscount(IFinalPrice cart, ref string view)
        {
            var coupon = new CouponDiscount(cart, _discountVariables);
            coupon.AdjustPriceByCouponDiscount();
            view += "Coupon discount adjustment  : $" + cart.GetAdjustment() * -1 + " \n";
            view += "Price after coupon discount : $" + cart.GetFinalPrice() + " \n";

            return coupon.AdjustedCart;
        }

#endregion

    }
}