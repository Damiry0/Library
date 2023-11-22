using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context;

public class ElibraryDbContext : DbContext
{
    public ElibraryDbContext()
    {
    }

    public ElibraryDbContext(DbContextOptions<ElibraryDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build()
            .GetConnectionString("Default");
        
        optionsBuilder.UseSqlServer(connectionString, options =>
        {
            options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        });
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasMany<Author>(s => s.Authors)
            .WithMany(c => c.Books);

        modelBuilder.Entity<Book>()
            .HasMany<Edition>(s => s.Editions)
            .WithOne(s => s.Book);

        modelBuilder.Entity<Department>()
            .HasMany<User>(s => s.Users)
            .WithOne(c => c.Department)
            .OnDelete(DeleteBehavior.NoAction);
        
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ElibraryDbContext).Assembly);
    }
    
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Borrowing> Borrowings { get; set; }
    public virtual DbSet<Edition> Editions { get; set; }
}
