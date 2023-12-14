using System;
using API.Models;
using BooksAPI.Triggers;
using Microsoft.EntityFrameworkCore;

namespace API.Context;

public class LibraryOracleDbContext : DbContext
{
    public LibraryOracleDbContext()
    {
    }

    public LibraryOracleDbContext(DbContextOptions<LibraryOracleDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build()
            .GetConnectionString("Oracle");
        
        optionsBuilder.UseOracle("User Id=system;Password=yourStrongPassword123;DATA SOURCE=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=192.168.0.101)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.UseTriggers(triggerOptions =>
        {
            triggerOptions.AddTrigger<BlockChangeUserNameTrigger>();
            
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)      
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryOracleDbContext).Assembly);
    }

    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Borrowing> Borrowings { get; set; }
    public virtual DbSet<Edition> Editions { get; set; }
}