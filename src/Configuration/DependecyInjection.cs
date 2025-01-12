using API.Repository;
using API.Services.Cloud.AWS;
using MySqlConnector;
using System.Text.Json;

namespace API.Configuration;

public static class DependecyInjection
{
    public static void DataBaseConnection(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<Connection>(x =>
        {
            if (Environment.GetEnvironmentVariable(nameof(Enviroment)) == nameof(Enviroment.DEV))
                return LoadDeveloperConnection();
            else 
            {
                var accessKey = builder.Configuration["Cloud:Aws:AccessKey"] ?? "";
                var secretKey = builder.Configuration["Cloud:Aws:SecretKey"] ?? "";
                var region = builder.Configuration["Cloud:Aws:Region"] ?? "us-east-1";

                return LoadImplantationConnection(accessKey, secretKey, region);
            }
        });
    }

    private static Connection LoadDeveloperConnection()
    {
        var conStr = new MySqlConnectionStringBuilder
        {
            Server = "",
            Password = "",
            UserID = "",
            Port = 0,
            Database = "",
        };

        return new Connection
        {
            _mysqlConnection = new MySqlConnection(conStr.ConnectionString)
        };
    }

    private static Connection LoadImplantationConnection(string accessKey, string secretKey, string region)
    {
        var jsonSecrets = new Secrets(accessKey, secretKey, region).GetDataBaseSecrets();
        var awsDataBaseSecrets = JsonSerializer.Deserialize<AWSDataBaseSecrets>(jsonSecrets);

        var conStr = new MySqlConnectionStringBuilder
        {
            Server = "",
            Password = "",
            UserID = "",
            Port = 0,
            Database = "",
        };

        return new Connection
        {
            _mysqlConnection = new MySqlConnection(conStr.ConnectionString)
        };
    }
}

internal class AWSDataBaseSecrets
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Server { get; set; } = null!;
    public string DataBase { get; set; } = null!;
}
