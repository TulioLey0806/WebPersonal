using WebPersonal_MVC;
using WebPersonal_MVC.Services;
using WebPersonal_MVC.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Adicionando el servicio de AutoMapper
builder.Services.AddAutoMapper(typeof(MappingConfig));

// Adicionando las Interfaces
builder.Services.AddHttpClient<ICategoriaCargoService, CategoriaCargoService>();
builder.Services.AddScoped<ICategoriaCargoService, CategoriaCargoService>();

builder.Services.AddHttpClient<IMunicipioService, MunicipioService>();
builder.Services.AddScoped<IMunicipioService, MunicipioService>();

builder.Services.AddHttpClient<IProvinciaService, ProvinciaService>();
builder.Services.AddScoped<IProvinciaService, ProvinciaService>();

builder.Services.AddHttpClient<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Implementación para el manejo de sesiones
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
