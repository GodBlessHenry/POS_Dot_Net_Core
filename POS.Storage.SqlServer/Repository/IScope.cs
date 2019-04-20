using System;

namespace POS.Storage.SqlServer.Repository
{
    public interface IScope : IDisposable
    {
        void Commit();
        void Rollback();
    }
}