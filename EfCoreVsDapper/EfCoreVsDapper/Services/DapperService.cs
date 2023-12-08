namespace EfCoreVsDapper.Services;

public class DapperService(IConfiguration configuration)
{
    private string ConnectionString { get; set; } =
        configuration.GetConnectionString("SqlServer") ?? string.Empty;

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        await using var connection = new SqlConnection(ConnectionString);
        return await connection.QueryAsync<User>("SELECT * FROM Users");
    }
}
