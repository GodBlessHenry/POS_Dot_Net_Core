namespace POS.Core
{
    public interface IShoppingCartCalculator
    {
        string CalculateFinalPrice(int orderId);
    }
}