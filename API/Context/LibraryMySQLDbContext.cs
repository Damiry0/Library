using API.Models;
using Laraue.EfCoreTriggers.MySql.Extensions;
using Microsoft.EntityFrameworkCore;

namespace API.Context;

public class LibraryMySQLDbContext : DbContext
{
    public LibraryMySQLDbContext()
    {
    }

    public LibraryMySQLDbContext(DbContextOptions<LibraryMySQLDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build()
            .GetConnectionString("MySQL");

        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.UseMySqlTriggers();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasMany(x => x.Books)
            .WithMany(x => x.Authors);

        modelBuilder.Entity<Department>()
            .HasMany<User>(s => s.Users)
            .WithOne(c => c.Department)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryMySQLDbContext).Assembly);
    }

    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Borrowing> Borrowings { get; set; }
    public virtual DbSet<Edition> Editions { get; set; }
}