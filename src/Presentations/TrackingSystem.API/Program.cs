using TrackingSystem.Shared.Configurations;

public class Startup
{
    private IConfiguration _Configuration { get; }
    private bool _IsForTests { get; init; }
    private JwtServiceConfig _JwtServiceConfig { get; init; }

    public Startup()
    {

    }

    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }
}