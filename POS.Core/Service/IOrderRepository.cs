namespace POS.Core.Service
{
    public interface IOrderRepository
    {
        Order GetById(int id);
    }
}