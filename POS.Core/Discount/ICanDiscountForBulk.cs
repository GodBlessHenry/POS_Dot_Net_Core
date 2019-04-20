namespace POS.Core.Services
{
    public interface ICanDiscountForBulk
    {
        decimal CalculateTotalOrderPrice(OrderItem subOrder);
    }
}