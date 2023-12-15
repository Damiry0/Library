using System;
using API.Models;
using BooksAPI.Triggers;
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
        optionsBuilder.LogTo(Console.WriteLine);
        var connectionString = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build()
            .GetConnectionString("MySQL");
        
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.UseTriggers(triggerOptions =>
        {
            triggerOptions.AddTrigger<BlockChangeUserNameTrigger>();
            
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)      
    {
        modelBuilder.Entity<User>().Navigation(e => e.Department).AutoInclude();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryMySQLDbContext).Assembly);
    }

    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Borrowing> Borrowings { get; set; }
    public virtual DbSet<Edition> Editions { get; set; }
}