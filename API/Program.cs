using System.Reflection;
using API.Context;
using API.Context.Repository;
using BooksAPI.Authentication;
using BooksAPI.Authentication.Jwt;
using BooksAPI.Exceptions.Filter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IJwtProvider, JwtProvider>();

builder.Services.AddDbContext<LibraryMsSQLDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSQL")));

var mySQLConnectionString = builder.Configuration.GetConnectionString("MySQL");
builder.Services.AddDbContext<LibraryMySQLDbContext>(options =>
    options.UseMySql(mySQLConnectionString, ServerVersion.AutoDetect(mySQLConnectionString)));

builder.Services.AddDbContext<LibraryOracleDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle")));

builder.Services.AddControllersWithViews(o => { o.Filters.Add<GlobalExceptionFilter>(); });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

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