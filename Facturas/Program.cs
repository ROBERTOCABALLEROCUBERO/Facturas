using Facturas.Mapper;
using Facturas.Repositories;
using Facturas.Repositorios.Implementaciones;
using Facturas.Repositorios.Interfaces;
using Facturas.Services;
using Facturas.Servicios.Implementaciones;
using Facturas.Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string CorsConfiguration = "_corsConfiguration";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<IFacturaService, FacturaService>();
builder.Services.AddScoped<ICrearFactura, CrearFactura>();

builder.Services.AddScoped<ILineaFacturaRepository, LineaFacturaRepository>();
builder.Services.AddScoped<ILineaFacturaService, LineaFacturaService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FacturasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FacturasContext")));


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsConfiguration,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(CorsConfiguration);

app.UseAuthorization();

app.MapControllers();

app.Run();
