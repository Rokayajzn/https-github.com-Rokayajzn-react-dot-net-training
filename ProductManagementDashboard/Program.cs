using DataLayer;

using Microsoft.EntityFrameworkCore;
using ProductManagementDashboard.Repository;

var builder = WebApplication.CreateBuilder(args);

// Controllers + JSON cycle handling (since you eager-load navs)
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Swagger **before** Build()
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB + DI
builder.Services.AddDbContext<DbModel>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("DataLayer")));

builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
