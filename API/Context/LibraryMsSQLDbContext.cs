using API.Models;
using Laraue.EfCoreTriggers.SqlServer.Extensions;
using Microsoft.EntityFrameworkCore;

namespace API.Context;

public class LibraryMsSQLDbContext : DbContext
{
    public LibraryMsSQLDbContext()
    {
    }

    public LibraryMsSQLDbContext(DbContextOptions<LibraryMsSQLDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build()
            .GetConnectionString("MsSQL");

        optionsBuilder.UseSqlServer(connectionString,
            options => { options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); });
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.UseSqlServerTriggers();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>()
            .HasMany<User>()
            .WithOne(c => c.Department)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryMsSQLDbContext).Assembly);
    }

    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Borrowing> Borrowings { get; set; }
    public virtual DbSet<Edition> Editions { get; set; }
}