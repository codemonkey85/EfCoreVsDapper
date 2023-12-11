namespace EfCoreVsDapper.Components.Pages;

public partial class Home
{
    List<User> DapperUsers = [];
    List<User> EfCoreUsers = [];

    private string Name { get; set; } = string.Empty;

    private string Email { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        DapperUsers = await UserRepository.GetAllUsers();
        EfCoreUsers = await AppDbContext.Users.ToListAsync();
    }

    private async Task AddUserEfCore()
    {
        var user = new User
        (
            0,
            Name,
            Email
        );

        await AppDbContext.AddAsync(user);
        await AppDbContext.SaveChangesAsync();

        Name = Email = string.Empty;

        DapperUsers = await UserRepository.GetAllUsers();
        EfCoreUsers = await AppDbContext.Users.ToListAsync();
    }

    private async Task AddUserDapper()
    {
        var user = new User
        (
            0,
            Name,
            Email
        );

        await UserRepository.AddUser(user);

        Name = Email = string.Empty;

        DapperUsers = await UserRepository.GetAllUsers();
        EfCoreUsers = await AppDbContext.Users.ToListAsync();
    }

    private async Task DeleteUserEfCore(int userId)
    {
        var user = await AppDbContext.Users.FindAsync(userId);

        if (user is not null)
        {
            AppDbContext.Users.Remove(user);
            await AppDbContext.SaveChangesAsync();
        }

        DapperUsers = await UserRepository.GetAllUsers();
        EfCoreUsers = await AppDbContext.Users.ToListAsync();
    }

    private async Task DeleteUserDapper(int userId)
    {
        await UserRepository.DeleteUser(userId);
        DapperUsers = await UserRepository.GetAllUsers();
        EfCoreUsers = await AppDbContext.Users.ToListAsync();
    }
}
