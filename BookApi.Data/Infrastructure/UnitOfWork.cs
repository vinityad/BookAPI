using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BookApi.Data.Infrastracture
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContextTransaction _transaction;
        
        private readonly AppDataContext _appDataContext;
        public DbContext DatabaseContext
        {
            get { return _appDataContext; }
        }

        public UnitOfWork(AppDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        #region Public Methods
        public void CommitTransaction()
        {
            SaveChanges();
            _transaction.Commit();
            _transaction.Dispose();
        }
        public void StartTransaction()
        {
            _transaction = DatabaseContext.Database.BeginTransaction();
        }
        public void RollBackTransaction()
        {
            _transaction.Rollback();
        }
        public void SaveChanges()
        {
            _appDataContext.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _appDataContext.SaveChangesAsync();
        }

        #endregion

        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DatabaseContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
