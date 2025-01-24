namespace API.Configuration;

public static class Configuration
{
    public static void LoadConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.GetSection("Configuration").Bind(ConfigFromAppSettings.SectionConfig);
    }
}
