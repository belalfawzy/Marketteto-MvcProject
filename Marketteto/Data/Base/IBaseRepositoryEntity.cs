namespace Marketteto.Data.Base
{
    public interface IBaseRepositoryEntity<T> where T :class,IBaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T Entity);
        Task UpdateAysnc(T Entity);
        Task DeleteAsync(int id);
    }
}
