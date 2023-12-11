using Dapper;

namespace EfCoreVsDapper.Data.Repositories;

public class UserRepository(AppDapperContext context) : IUserRepository
{
    private AppDapperContext Context { get; } = context;

    public async Task<List<User>> GetAllUsers()
    {
        using var connection = Context.CreateConnection();
        return (await connection.QueryAsync<User>("""
            SELECT *
            FROM Users
            """
            )).ToList();
    }

    public async Task<User?> GetUser(int userId)
    {
        using var connection = Context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<User>("""
            SELECT *
            FROM Users
            WHERE Id = @Id
            """,
            new
            {
                Id = userId
            });
    }

    public async Task AddUser(User user)
    {
        using var connection = Context.CreateConnection();
        await connection.ExecuteAsync("""
            INSERT INTO Users (Name, Email)
            VALUES (@Name, @Email)
            """,
            new
            {
                Name = user.Name,
                Email = user.Email
            });
    }

    public async Task UpdateUser(User user)
    {
        using var connection = Context.CreateConnection();
    }

    public async Task DeleteUser(int userId)
    {
        using var connection = Context.CreateConnection();
        await connection.ExecuteAsync("""
            DELETE
            FROM Users
            WHERE Id = @Id
            """,
            new
            {
                Id = userId
            });
    }
}
