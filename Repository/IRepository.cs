namespace VnStockproxx
{
    public interface IRepository<T>
    {
        Task Add(T entity);
        bool Exist(int id);
        Task Update(T entity);
        Task Remove(T entity);
        Task<T> FindById(int id);
        Task<List<T>> GetAll();
    }
}
