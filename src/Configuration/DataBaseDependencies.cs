using API.Repository;
using API.Services.Cloud.AWS;
using MySqlConnector;
using System.Text.Json;

namespace API.Configuration;

public static class DataBaseDependencies
{
    public static void LoadDataBaseConnection(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<Connection>(x =>
        {
            if (Environment.GetEnvironmentVariable(nameof(Enviroment)) == nameof(Enviroment.dev))
            {
                var dataBaseCredentials = new DataBaseCredentials
                {
                    DataBase = builder.Configuration["DataBaseConnection:database"] ?? "quem-indica",
                    Server = builder.Configuration["DataBaseConnection:server"] ?? "localhost",
                    Password = builder.Configuration["DataBaseConnection:password"] ?? "sinqia123",
                    Port = uint.Parse(builder.Configuration["DataBaseConnection:port"] ?? "3306"),
                    UserID = builder.Configuration["DataBaseConnection:userId"] ?? "root",
                };

                return LoadDeveloperConnection(dataBaseCredentials);
            }
            else 
            {
                var accessKey = builder.Configuration["Cloud:Aws:AccessKey"] ?? "";
                var secretKey = builder.Configuration["Cloud:Aws:SecretKey"] ?? "";
                var region = builder.Configuration["Cloud:Aws:Region"] ?? "us-east-1";

                return LoadImplantationConnection(accessKey, secretKey, region);
            }
        });
    }

    private static Connection LoadDeveloperConnection(DataBaseCredentials dataBaseCredentials)
    {
        var conStr = new MySqlConnectionStringBuilder
        {
            Server = dataBaseCredentials.Server,
            Password = dataBaseCredentials.Password,
            UserID = dataBaseCredentials.UserID,
            Port = dataBaseCredentials.Port,
            Database = dataBaseCredentials.DataBase,
        };

        return new Connection
        {
            _mysqlConnection = new MySqlConnection(conStr.ConnectionString)
        };
    }

    private static Connection LoadImplantationConnection(string accessKey, string secretKey, string region)
    {
        var jsonSecrets = new Secrets(accessKey, secretKey, region).GetDataBaseSecrets();
        var awsDataBaseSecrets = JsonSerializer.Deserialize<DataBaseCredentials>(jsonSecrets);

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

internal class DataBaseCredentials
{
    public string UserID { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Server { get; set; } = null!;
    public string DataBase { get; set; } = null!;
    public uint Port { get; set; }
}
