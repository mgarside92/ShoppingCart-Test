using Microsoft.EntityFrameworkCore;
using ShoppingCart.AppStart;
using ShoppingCart.Domain.Services.Interfaces;
using ShoppingCart.Repositories.DAL;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var connectionString = config.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterServices();

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

using (var scope = app.Services.CreateScope())
{
    var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
    var dealService = scope.ServiceProvider.GetRequiredService<IDealService>();

    productService.SeedProducts();
    dealService.SeedDeals();
}

app.Run();
