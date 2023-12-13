using BooksAPI.Enums;
using Microsoft.EntityFrameworkCore;

namespace API.Context.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly LibraryMsSQLDbContext _contextMSSQL;
    private readonly LibraryMySQLDbContext _contextMySQL;

    public Repository(LibraryMsSQLDbContext contextMssql, LibraryMySQLDbContext contextMySql)
    {
        _contextMSSQL = contextMssql;
        _contextMySQL = contextMySql;
    }

    IQueryable<TEntity> IRepository<TEntity>.GetAll()
    {
        return _contextMSSQL.Set<TEntity>().Union(_contextMySQL.Set<TEntity>());
    }

    IQueryable<TEntity> IRepository<TEntity>.GetAllAsNoTracking()
    {
        return _contextMSSQL.Set<TEntity>().Union(_contextMySQL.Set<TEntity>().AsNoTracking()).AsNoTracking();
    }

    public async Task AddAsync(TEntity entity, DataCenter? dataCenter)
    {
        switch (dataCenter)
        {
            case DataCenter.MsSql:
                await _contextMSSQL.Set<TEntity>().AddAsync(entity);
                break;
            case DataCenter.MySql:
                await _contextMySQL.Set<TEntity>().AddAsync(entity);
                break;
            default:
                await _contextMSSQL.Set<TEntity>().AddAsync(entity);
                await _contextMySQL.Set<TEntity>().AddAsync(entity);
                break;
        }
    }

    public void Delete(TEntity entity, DataCenter? dataCenter)
    {
        switch (dataCenter)
        {
            case DataCenter.MsSql:
                _contextMSSQL.Remove(entity);
                break;
            case DataCenter.MySql:
                _contextMySQL.Remove(entity);
                break;
            default:
                _contextMSSQL.Remove(entity);
                _contextMySQL.Remove(entity);
                break;
        }
    }

    public async Task SaveAsync(DataCenter? dataCenter)
    {
        switch (dataCenter)
        {
            case DataCenter.MsSql:
                await _contextMSSQL.SaveChangesAsync();
                break;
            case DataCenter.MySql:
                await _contextMySQL.SaveChangesAsync();
                break;
            default:
                await _contextMSSQL.SaveChangesAsync();
                await _contextMySQL.SaveChangesAsync();
                break;
        }
    }
}