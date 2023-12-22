using BooksAPI.Enums;
using Microsoft.EntityFrameworkCore;

namespace API.Context.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly LibraryMsSQLDbContext _contextMSSQL;
    private readonly LibraryMySQLDbContext _contextMySQL;
    private readonly LibraryOracleDbContext _contextOracle;

    public Repository(LibraryMsSQLDbContext contextMssql, LibraryMySQLDbContext contextMySql,
        LibraryOracleDbContext contextOracle)
    {
        _contextMSSQL = contextMssql;
        _contextMySQL = contextMySql;
        _contextOracle = contextOracle;
    }

    IEnumerable<TEntity> IRepository<TEntity>.GetAll()
    {
        var mssql = _contextMSSQL.Set<TEntity>().AsTracking().AsEnumerable();
        var mysql = _contextMySQL.Set<TEntity>().AsTracking().AsEnumerable();
        var oracle = _contextOracle.Set<TEntity>().AsTracking().AsEnumerable();
        return _contextMSSQL.Set<TEntity>().AsTracking().AsEnumerable();
    }

    IEnumerable<TEntity> IRepository<TEntity>.GetAllAsNoTracking()
    {
        var mssql = _contextMSSQL.Set<TEntity>().AsNoTracking().AsEnumerable();
        var mysql = _contextMySQL.Set<TEntity>().AsNoTracking().AsEnumerable();
        var oracle = _contextOracle.Set<TEntity>().AsTracking().AsEnumerable();
        return mysql.Union(mssql).Union(oracle);
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
            case DataCenter.Oracle:
                await _contextOracle.Set<TEntity>().AddAsync(entity);
                break;
            case null:
                await _contextMSSQL.Set<TEntity>().AddAsync(entity);
                await _contextMySQL.Set<TEntity>().AddAsync(entity);
                await _contextOracle.Set<TEntity>().AddAsync(entity);
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
            case DataCenter.Oracle:
                _contextOracle.Remove(entity);
                break;
            case null:
                _contextMSSQL.Remove(entity);
                _contextMySQL.Remove(entity);
                _contextOracle.Remove(entity);
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
            case DataCenter.Oracle:
                await _contextOracle.SaveChangesAsync();
                break;
            case null:
                await _contextMSSQL.SaveChangesAsync();
                await _contextMySQL.SaveChangesAsync();
                await _contextOracle.SaveChangesAsync();
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
            case DataCenter.Oracle:
                _contextOracle.Attach(entity);
                break;
            case null:
                _contextMSSQL.Attach(entity);
                _contextMySQL.Attach(entity);
                _contextOracle.Attach(entity);
                break;
        }
    }
}