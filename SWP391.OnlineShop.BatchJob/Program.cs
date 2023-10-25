using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog;
using SWP391.OnlineShop.BatchJob.Configs.HangFire;
using SWP391.OnlineShop.BatchJob.Jobs.Implements;
using SWP391.OnlineShop.BatchJob.Jobs.Interfaces;
using SWP391.OnlineShop.Common.Constraints;
using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Customs.Environments;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.Core.Models.Settings;
using SWP391.OnlineShop.ServiceInterface.Emails;
using SWP391.OnlineShop.ServiceInterface.Loggers;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

var path = Directory.GetCurrentDirectory();
var logPath = Path.Combine(path, "Logs");

GlobalDiagnosticsContext.Set("LogDirectory", logPath);
LogManager.Setup().LoadConfigurationFromFile(string.Concat(path, @"\NLog.config"));

// System services
services.AddControllersWithViews();

// Add DbContext service
services.AddDbContext<OnlineShopContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("SWP391.OnlineShop"));
});

services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

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
        //options.SignIn.RequireConfirmedEmail = true;
    })
    .AddEntityFrameworkStores<OnlineShopContext>()
    .AddDefaultTokenProviders();

// Add defaultCookie services
services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Account/ErrorForbidden";
    options.LoginPath = "/Account/Login";
    options.Cookie.Name = "OnlineShopUser";
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
});

// Add services to the container.
services.AddHangfire(c =>
{
    c.UseSimpleAssemblyNameTypeSerializer();
    c.UseRecommendedSerializerSettings();
    c.UseSqlServerStorage(config.GetConnectionString("SWP391.OnlineShop.HangFire"));
});

services.AddHangfireServer();

// Add others service
// Logging services
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<ILoggerService, LoggerService>();
services.AddScoped<IMailService, MailService>();

// Job services
services.AddScoped<IJobRegistration, RecurringRegistrationJob>();
services.AddScoped<IJobRegistration, RecurringRegistrationWindowServiceJob>();

services.AddScoped<IJobWebService, SendMailJob>();

// Configs Smtp
services.Configure<Smtp>(config.GetSection("Smtp"));

// Configs Environment
services.Configure<DeveloperEnvironment>(config.GetSection("DeveloperEnvironment"));

// AutoMapper service
services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "route",
    pattern: "{controller}/{action}/{id?}");

// Hang-fire application control
var authorize = new HangFireAuthorizationFilter();

var options = new DashboardOptions
{
    Authorization = new[] { authorize }
};

var isDeveloperEnvironment = config.GetSection("DeveloperEnvironment:Enable")?.Value;
if (isDeveloperEnvironment != null && Convert.ToBoolean(isDeveloperEnvironment))
{
    options = new DashboardOptions
    {
        //IsReadOnlyFunc = context => !authorize.IsReadOnlyHangFireDashboard(context),
    };
}

app.UseHangfireDashboard("/hangfire", options);
app.MapHangfireDashboard();

var job = new RecurringRegistrationWindowServiceJob(null, null) as IJobRegistration;
RecurringJob.AddOrUpdate<IJobRegistration>("WebService Hosted JobRegistration",
    r => job.InitiateJob(),
    "35 15 31 12 *");

app.Run();
