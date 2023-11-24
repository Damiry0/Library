using API.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BooksAPI.IntegrationTests;

public class ApiTestBase
{
    private SqliteConnection _connection;


    protected virtual void AddTestServices(IServiceCollection services)
    {
        services.AddDbContext<LibraryDbContext>(options => { options.UseSqlServer(_connection); });
    }
}