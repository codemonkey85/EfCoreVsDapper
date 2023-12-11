namespace EfCoreVsDapper.Data.Repositories;

public interface IUserRepository
{
    public Task<List<User>> GetAllUsers();

    public Task<User?> GetUser(int userId);

    public Task AddUser(User user);

    public Task UpdateUser(User user);

    public Task DeleteUser(int userId);
}
