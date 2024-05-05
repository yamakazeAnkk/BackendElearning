namespace ElearningFake.Contracts
{
    public interface IRepository<T, V, U> where T : class
                                          where V : class
                                          where U : class
        // Example T is Classroom, V is ClassroomModel, U is ClassroomDTO
    {
        Task<List<V>> GetAllAsync();
        Task<V> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(V id);
    }
}
