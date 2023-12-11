using Dapper;

namespace EfCoreVsDapper.Data.Repositories;

public class UserRepository(AppDapperContext context) : IUserRepository
{
    private AppDapperContext Context { get; } = context;

    public async Task<List<User>> GetAllUsers()
    {
        using var connection = Context.CreateConnection();
        return (await connection.QueryAsync<User>(
            "SELECT * FROM Users")).ToList();
    }

    public async Task<User?> GetUser(int userId)
    {
        using var connection = Context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Id = @Id",
            new
            {
                Id = userId
            });
    }
}
