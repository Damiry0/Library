using System.Reflection;
using API.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryMsSQLDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSQL")));

var mySQLConnectionString = builder.Configuration.GetConnectionString("MySQL");
builder.Services.AddDbContext<LibraryMySQLDbContext>(options =>
    options.UseMySql(mySQLConnectionString, ServerVersion.AutoDetect(mySQLConnectionString)));


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