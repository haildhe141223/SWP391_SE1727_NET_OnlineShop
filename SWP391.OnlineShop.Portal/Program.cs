using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog;
using ServiceStack;
using SWP391.OnlineShop.Common.Constraints;
using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.Core.Models.Settings;
using SWP391.OnlineShop.ServiceInterface.Loggers;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

builder.WebHost.ConfigureKestrel(c =>
{
    c.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(30);
});

var path = Directory.GetCurrentDirectory();
var logPath = Path.Combine(path, "Logs");

GlobalDiagnosticsContext.Set("LogDirectory", logPath);
LogManager.Setup().LoadConfigurationFromFile(string.Concat(path, @"\NLog.config"));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Cors service
services.AddCors(options =>
    options.AddPolicy("CorsSettings",
        p =>
        {
            p.WithOrigins("*");
            p.AllowAnyMethod();
            p.AllowAnyHeader();
        }));

// Add authentication services
services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        var googleConfig = config.GetSection("Authentication:Google");

        googleOptions.ClientId = googleConfig["ClientId"];
        googleOptions.ClientSecret = googleConfig["ClientSecret"];
        // https://localhost:host/signin-google
        googleOptions.CallbackPath = "/signin-google";
    });

// Add DbContext
services.AddDbContext<OnlineShopContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("SWP391.OnlineShop"));
});

services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
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
    options.ExpireTimeSpan = TimeSpan.FromDays(3);
});

// Add time expired for identity token
services.Configure<DataProtectionTokenProviderOptions>(options =>
    options.TokenLifespan = TimeSpan.FromMinutes(30));

// Add ServiceStack services
services.AddScoped<IJsonServiceClient>(_ => new JsonServiceClient(config["ServiceApi"]));

// Add NLog services
services.AddScoped<ILoggerService, LoggerService>();
services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configs Smtp
services.Configure<Smtp>(config.GetSection("Smtp"));

var app = builder.Build();

app.UseCors("CorsSettings");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    var db = new OnlineShopDbInitializer();
    db.Seed();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
//    RequestPath = "/uploads"
//});

app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCookiePolicy(new CookiePolicyOptions()
{
    MinimumSameSitePolicy = SameSiteMode.Lax
});

app.UseExceptionHandler("/Home/Error");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
