using System.Data;
using EntityFrameworkConcurrency01.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EntityFrameworkConcurrency01
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Serializable);
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly BloggingContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(BloggingContext context)
        {
            _context = context;
        }
         void IUnitOfWork.Commit()
        {
            _transaction.Commit();
        }

        void IUnitOfWork.Rollback()
        {
            _transaction.Rollback();
        }

        void IUnitOfWork.BeginTransaction(IsolationLevel isolationLevel)
        {
            if (_transaction != null) return;
            _transaction = _context.Database.BeginTransaction(isolationLevel);
        }
    }
}
