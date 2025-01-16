using API.Middleware;
using API.Repository.Abstraction;
using API.Repository.Implementation;

namespace API.Configuration;

public static class ServicesInjection
{
    public static void LoadDependencies(this WebApplicationBuilder builder)
    {
        LoadRepositories(builder);
        LoadMiddlewares(builder);
    }

    private static void LoadRepositories(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAddressRepository, MysqlAddressRepository>();
        builder.Services.AddScoped<IClientRepository, MysqlClientRepository>();
    }

    private static void LoadMiddlewares(WebApplicationBuilder builder)
    {
        var mysqk = builder.Services.BuildServiceProvider().GetRequiredService<IClientRepository>();

        builder.Services.AddScoped<ClientMid>();
        builder.Services.AddScoped<AddressMid>();
    }
}
