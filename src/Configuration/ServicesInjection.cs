using API.Middleware;
using API.Repository;
using API.Repository.Implementation;

namespace API.Configuration;

public static class ServicesInjection
{
    public static void LoadDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ClientMid>(x =>
        {
            Connection connection = builder.Services.BuildServiceProvider().GetRequiredService<Connection>();

            return new ClientMid(new MysqlClientRepository(connection));
        });
    }
}
