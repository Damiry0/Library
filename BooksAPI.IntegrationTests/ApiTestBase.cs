using API.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BooksAPI.IntegrationTests;

public class ApiTestBase<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    private SqliteConnection _connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<LibraryMsSQLDbContext>));

            services.Remove(descriptor);

            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            services.AddDbContext<LibraryMsSQLDbContext>(options => { options.UseSqlite(_connection); });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<LibraryMsSQLDbContext>();

            db.Database.EnsureCreated();
        });
    }

    public HttpClient GetClient => CreateClient();

    protected virtual void AddTestServices(IServiceCollection services)
    {
        services.AddDbContext<LibraryMsSQLDbContext>(options => { options.UseSqlServer(_connection); });
    }
}