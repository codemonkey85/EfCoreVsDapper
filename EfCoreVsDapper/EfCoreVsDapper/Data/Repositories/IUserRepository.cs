namespace EfCoreVsDapper.Data.Repositories;

public interface IUserRepository
{
    public Task<List<User>> GetAllUsers();

    public Task<User?> GetUser(int userId);
}
