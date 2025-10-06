using DataLayer;

using Microsoft.EntityFrameworkCore;






var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbModel>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("DataLayer")));


//builder.Services.AddScoped<UserRepository>();
//// Program.cs
//builder.Services.AddScoped<CustomPasswordHasher>();

//// Add the default PasswordHasher for your custom User type

//builder.Services.AddScoped<RoleRepository>();
//builder.Services.AddScoped<UserRoleRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
