using System;
using System.Threading.Tasks;

namespace BookApi.Data.Infrastracture
{
    public interface IUnitOfWork: IDisposable
    {
        void CommitTransaction();
        void StartTransaction();
        void RollBackTransaction();
        void SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
