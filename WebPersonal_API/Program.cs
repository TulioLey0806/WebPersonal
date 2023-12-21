using Microsoft.EntityFrameworkCore;
using WebPersonal_API;
using WebPersonal_API.Datos;
using WebPersonal_API.Repositorio;
using WebPersonal_API.Repositorio.IRepositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Activando la cadena de conexión
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnetion"));
});

// Implementando AutoMapper
builder.Services.AddAutoMapper(typeof(MappingConfig));

// Activando la Interface ICategoriaCargoRepositorio
builder.Services.AddScoped<ICategoriaCargoRepositorio, CategoriaCargoRepositorio>();

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
