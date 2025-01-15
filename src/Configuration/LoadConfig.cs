namespace API.Configuration;

public static class LoadConfig
{
    public static void LoadAppSettings(this WebApplicationBuilder builder)
    {
        var env = Environment.GetEnvironmentVariable(nameof(Enviroment)) ?? "dev";
        if (env is not null)
            env = env.ToLower();

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{env}.json", false, true)
            .Build();

        builder.Configuration.AddConfiguration(configuration);
    }
}
