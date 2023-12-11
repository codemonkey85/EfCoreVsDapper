namespace EfCoreVsDapper.Data;

public class AppDapperContext(IConfiguration configuration)
{
    private string ConnectionString { get; set; } =
        configuration.GetConnectionString("SqlServer") ?? string.Empty;

    public IDbConnection CreateConnection() =>
        new SqlConnection(ConnectionString);
}
