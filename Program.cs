using BackendTuya.src.Api.Middlewares;
using BackendTuya.src.Application.Customers.Commands;
using BackendTuya.src.Application.Orders;
using BackendTuya.src.Domain.Customers;
using BackendTuya.src.Domain.Orders;
using BackendTuya.src.Infrastructure.Persistence;
using BackendTuya.src.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// EF Core
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repos (Adapters)
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Application services
builder.Services.AddScoped<IOrderService, OrderService>();

// MediatR (Handlers en Application)
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<CreateCustomerCommandHandler>(); // Mayor claridad (No necesitamos mas de un Assembly)
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BackendTuya API",
        Version = "v1"
    });
});

var app = builder.Build();

// Intentar crear DB solo si hay connection string y el servidor está disponible
try
{
    var cs = builder.Configuration.GetConnectionString("DefaultConnection");

    if (!string.IsNullOrWhiteSpace(cs))
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Verifica conectividad antes de EnsureCreated para no tumbar la app
        if (await db.Database.CanConnectAsync())
        {
            await db.Database.EnsureCreatedAsync();
        }
        else
        {
            app.Logger.LogWarning("No se pudo conectar a la base de datos. La API iniciará sin DB. Verifique DefaultConnection.");
        }
    }
    else
    {
        app.Logger.LogWarning("DefaultConnection no configurada. La API iniciará sin DB.");
    }
}
catch (Exception ex)
{
    app.Logger.LogWarning(ex, "Fallo al inicializar la DB. La API iniciará sin DB.");
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    // Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackendTuya API v1");
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
