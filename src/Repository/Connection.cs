using MySqlConnector;

namespace API.Repository;

public sealed class Connection
{
    public MySqlConnection _mysqlConnection { get; set; } = null!;
}
