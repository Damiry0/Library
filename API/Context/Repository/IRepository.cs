using BooksAPI.Enums;

namespace API.Context.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();
    IEnumerable<TEntity> GetAllAsNoTracking();
    Task AddAsync(TEntity entity, DataCenter? dataCenter);
    void Delete(TEntity entity, DataCenter? dataCenter);
    Task SaveAsync(DataCenter? dataCenter);
    void Attach(TEntity entity, DataCenter? dataCenter);
}