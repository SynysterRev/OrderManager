using Microsoft.EntityFrameworkCore;
using OrderManager.Application.Interfaces;
using OrderManager.Application.Services;
using OrderManager.Domain.Repositories;
using OrderManager.Infrastructure.DatabaseContext;
using OrderManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IOrderItemsService, OrderItemsService>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));

    options.SwaggerDoc("v1", new() { Title = "OrderManager API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHsts();
app.UseHttpsRedirection();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderManager API V1");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
