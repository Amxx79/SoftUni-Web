namespace CinemaApp.Data.Repository.Interfaces
{
    public interface IRepository<TType, TId>
    {
        TType GetById(TId id);
        Task<TType> GetByIdAsync(TId id);
        IEnumerable<TType> GetAll();
        Task<IEnumerable<TType>> GetAllAsync();
        IEnumerable<TType> GetAllAttached();
        bool Add(TType item);
        Task<bool> AddAsync(TType item);
        bool Delete(TId id);
        Task<bool> DeleteAsync(TId id);
        bool Update(TType item);
        Task<bool> UpdateAsync(TType item);
    }
}
