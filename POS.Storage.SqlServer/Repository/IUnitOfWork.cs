namespace POS.Storage.SqlServer.Repository
{
    public interface IUnitOfWork
    {
        IScope Begin();
    }
}
