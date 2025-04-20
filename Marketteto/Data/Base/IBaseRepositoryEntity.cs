using System.Linq.Expressions;

namespace Marketteto.Data.Base
{
    public interface IBaseRepositoryEntity<T> where T :class,IBaseEntity
    {
        Task<List<T>> GetAllAsync();
        // ***************************************
        Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] include);

        // *****************************************
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] include);
        Task CreateAsync(T Entity);
        Task UpdateAysnc(T Entity);
        Task DeleteAsync(int id);
    }
}
