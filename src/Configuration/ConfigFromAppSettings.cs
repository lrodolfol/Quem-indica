namespace API.Configuration;

public static class ConfigFromAppSettings
{
    public static Configuration SectionConfig { get; set; } = new();
    public class Configuration
    {
        public uint DaysForOverdue { get; set; }
    }
}
