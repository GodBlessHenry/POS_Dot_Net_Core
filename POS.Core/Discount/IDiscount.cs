namespace POS.Core.Services
{
    public interface IDiscount
    {
        decimal TotalAfterDiscount { get; }

        decimal CalculateDiscountPrice();
    }
}