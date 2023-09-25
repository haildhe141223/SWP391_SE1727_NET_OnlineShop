using Funq;
using ServiceStack;
using ServiceStack.Api.OpenApi;
using SWP391.OnlineShop.ServiceInterface.BaseServices;

namespace SWP391.OnlineShop.Service;

public class AppHost : AppHostBase, IAppHost
{
    public AppHost(string licenseKey) : base("OSP Api Webservice",
        typeof(AppHost).Assembly,
        typeof(BaseService).Assembly)
    {
        Licensing.RegisterLicense(licenseKey);
    }

    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services =>
        {
            // Configure ASP.NET Core IOC Dependencies
        });

    public override void Configure(Container container)
    {
        Plugins.Add(new OpenApiFeature());

        SetConfig(new HostConfig
        {
        });
    }
}