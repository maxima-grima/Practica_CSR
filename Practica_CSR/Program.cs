using Microsoft.EntityFrameworkCore;
using Practica_CSR.Data;
using Practica_CSR.Repositories.Implementations;
using Practica_CSR.Repositories.Interfaces;
using Practica_CSR.Services.Implementations;
using Practica_CSR.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Practica_CSRContext>(dbContextOptions => dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:Practica_CSRDBConnectionString"]));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

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