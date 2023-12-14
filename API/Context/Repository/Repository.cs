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

    IEnumerable<TEntity> IRepository<TEntity>.GetAllAsNoTracking()
    {
        var mssql = _contextMSSQL.Set<TEntity>().AsNoTracking().ToList();
        var mysql = _contextMySQL.Set<TEntity>().AsNoTracking().ToList();
        return mysql.Union(mssql);
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
            case null:
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
            case null:
                _contextMSSQL.Remove(entity);
                _contextMySQL.Remove(entity);
                break;
        }
    }

    public async Task SaveAsync(DataCenter? dataCenter)
    {
        _contextMSSQL.ChangeTracker.DetectChanges();
        Console.WriteLine(_contextMSSQL.ChangeTracker.DebugView.LongView);
        switch (dataCenter)
        {
            case DataCenter.MsSql:
                await _contextMSSQL.SaveChangesAsync();
                break;
            case DataCenter.MySql:
                await _contextMySQL.SaveChangesAsync();
                break;
            case null:
                await _contextMSSQL.SaveChangesAsync();
                await _contextMySQL.SaveChangesAsync();
                break;
        }
    }

    public void Attach(TEntity entity, DataCenter? dataCenter)
    {
        switch (dataCenter)
        {
            case DataCenter.MsSql:
                 _contextMSSQL.Attach(entity);
                break;
            case DataCenter.MySql:
                _contextMySQL.Attach(entity);
                break;
            case null:
                _contextMSSQL.Attach(entity);
                _contextMySQL.Attach(entity);
                break;
        }
    }
}