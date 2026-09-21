//global using SupplierCapabilitiesAndManagementSystem.Models.DTO;
//global using SupplierCapabilitiesAndManagementSystem.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SupplierCapabilitiesAndManagementSystem.Database;
using SupplierCapabilitiesAndManagementSystem.Entities;
using SupplierCapabilitiesAndManagementSystem.Repositories;
using SupplierCapabilitiesAndManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierMachineRepository, SupplierMachineRepository>();
builder.Services.AddScoped<ISupplierCertificateRepository, SupplierCertificateRepository>();
builder.Services.AddScoped<ISupplierHumanResourceRepository, SupplierHumanResourceRepository>();
builder.Services.AddScoped<IQualitierRepository, QualitierRepository>();
builder.Services.AddScoped<IPurchaserRepository, PurchaserRepository>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ISupplierMachineService, SupplierMachineService>();
builder.Services.AddScoped<ISupplierCertificateService, AddCertificateService>();
builder.Services.AddScoped<ISupplierHumanResourceService, SupplierHumanResourceService>();
builder.Services.AddScoped<IQualitierService, QualitierService>();
builder.Services.AddScoped<IPurchaserService, PurchaserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/auth/login";
    options.AccessDeniedPath = "/auth/access-denied";
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
