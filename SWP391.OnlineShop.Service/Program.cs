using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog;
using ServiceStack;
using SWP391.OnlineShop.Common.Constraints;
using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.Core.Models.Settings;
using SWP391.OnlineShop.Service;
using SWP391.OnlineShop.Service.Configs.AutoMapper;
using SWP391.OnlineShop.ServiceInterface.Emails;
using SWP391.OnlineShop.ServiceInterface.Loggers;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;
var serviceStackLicense = config.GetSection("ServiceStack:LicenseKey").Value;

builder.WebHost.ConfigureKestrel(c =>
{
    c.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(30);
});

var path = Directory.GetCurrentDirectory();
var logPath = Path.Combine(path, "Logs");

GlobalDiagnosticsContext.Set("LogDirectory", logPath);
LogManager.Setup().LoadConfigurationFromFile(string.Concat(path, @"\NLog.config"));

// Add services to the container.
builder.Services.AddRazorPages();
services.AddControllers();

// Cors service
services.AddCors(options =>
    options.AddPolicy("CorsSettings",
        p =>
        {
            p.WithOrigins("*");
            p.AllowAnyMethod();
            p.AllowAnyHeader();
        }));

// Add DbContext
services.AddDbContext<OnlineShopContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("SWP391.OnlineShop"));
});

// Add services to the container.
services.AddHangfire(c =>
{
    c.UseSimpleAssemblyNameTypeSerializer();
    c.UseRecommendedSerializerSettings();
    c.UseSqlServerStorage(config.GetConnectionString("SWP391.OnlineShop.HangFire"));
});

services.AddHangfireServer();

// Add identity configs
services.AddIdentity<User, Role>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = true;
        options.User.AllowedUserNameCharacters = string.Join("",
            LoginKeyConstraints.VietnameseDictionary);

        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = true;
    })
    .AddEntityFrameworkStores<OnlineShopContext>()
    .AddDefaultTokenProviders();

// Configs dependence inject
services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configs logging
services.AddScoped<ILoggerService, LoggerService>();
services.AddScoped<IMailService, MailService>();

// Configs setting
services.Configure<Smtp>(config.GetSection("Smtp"));

// AutoMapper service
//services.AddAutoMapper(typeof(Program));
services.AddScoped(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfigs(provider.GetService<IUnitOfWork>()));
}).CreateMapper());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseCors("CorsSettings");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseServiceStack(new AppHost(serviceStackLicense));

app.MapControllers();

app.Run();
