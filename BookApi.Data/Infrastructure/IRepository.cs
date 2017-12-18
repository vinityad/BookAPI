using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookApi.Data.Infrastracture
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();

        TEntity GetByID(object id);
        Task<TEntity> GetByIDAsync(object id);

        void Insert(TEntity entity);

        void Delete(object id);
        Task DeleteAsync(object id);
        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
        Task UpdateAsync(TEntity entityToUpdate);
    }
}
