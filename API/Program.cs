using System.Reflection;
using API.Context;
using API.Context.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddDbContext<LibraryMsSQLDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSQL")));

var mySQLConnectionString = builder.Configuration.GetConnectionString("MySQL");
builder.Services.AddDbContext<LibraryMySQLDbContext>(options =>
    options.UseMySql(mySQLConnectionString, ServerVersion.AutoDetect(mySQLConnectionString)));

var oracleConnectionString = builder.Configuration.GetConnectionString("Oracle");
builder.Services.AddDbContext<LibraryOracleDbContext>(options =>
    options.UseOracle("User Id=system;Password=yourStrongPassword123;DATA SOURCE=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=192.168.0.101)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();