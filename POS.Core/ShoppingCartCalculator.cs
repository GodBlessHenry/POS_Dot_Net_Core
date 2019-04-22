using POS.Core.Discount;
using POS.Core.Service;

namespace POS.Core
{
    // This class is for being called by the web API controller,
    // Since the web api project is not required for the assignment,
    // hence I didn't include the web api project, although I did use it for testing.
    // And also I didn't do unit test for this class since it's just
    // for showing how the calculation can be done and not include
    // much of business logic. 
    public class ShoppingCartCalculator : IShoppingCartCalculator
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDiscountVariablesRepository _discountVariablesRepository;

        public ShoppingCartCalculator(IDiscountVariablesRepository discountVariablesRepository, IOrderRepository orderRepository)
        {
            _discountVariablesRepository = discountVariablesRepository;
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
            var bulkDiscount = new BulkDiscount(cart, _discountVariablesRepository);
            bulkDiscount.AdjustPriceByBulkDiscount();
            view += "Bulk discount adjustment    : $" + cart.GetAdjustment() * -1 + " \n";
            view += "Price after bulk discount   : $" + cart.GetFinalPrice() + " \n";

            return bulkDiscount.AdjustedCart;
        }

        private IFinalPrice UseCouponDiscount(IFinalPrice cart, ref string view)
        {
            var coupon = new CouponDiscount(cart, _discountVariablesRepository);
            coupon.AdjustPriceByCouponDiscount();
            view += "Coupon discount adjustment  : $" + cart.GetAdjustment() * -1 + " \n";
            view += "Price after coupon discount : $" + cart.GetFinalPrice() + " \n";

            return coupon.AdjustedCart;
        }

#endregion

    }
}