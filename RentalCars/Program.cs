using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RentalCars.Data;
using RentalCars.Handlers;

var builder = WebApplication.CreateBuilder(args);

//Configur DB
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional : false, reloadOnChange : true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional:true)
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<RegistrasiHandler>();
builder.Services.AddScoped<HomeHandler>();
builder.Services.AddScoped<RentCarHandler>();
builder.Services.AddScoped<HistoryRentHandler>();

// Layanan autentikasi
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Path ke halaman login
        options.LogoutPath = "/Logout"; // Path ke halaman logout
        options.AccessDeniedPath = "/Home/AccessDenied"; // Path ke halaman akses ditolak (opsional)
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

//Autentikasi dan Autorisasi
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
