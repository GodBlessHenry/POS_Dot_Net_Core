using System;
using Microsoft.EntityFrameworkCore;

namespace POS.Storage.SqlServer.Repository
{
    public class UnitOfWork : IUnitOfWork, IScope
    {
        private readonly DbContext _context;
        private bool _committed;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public IScope Begin()
        {
            _committed = false;
            return this;
        }

        public void Commit()
        {
            _context.SaveChanges();
            _committed = true;
        }

        public void Rollback()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (!_committed)
            {
                Rollback();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}