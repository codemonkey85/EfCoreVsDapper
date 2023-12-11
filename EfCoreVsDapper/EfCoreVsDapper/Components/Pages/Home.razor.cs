namespace EfCoreVsDapper.Components.Pages;

public partial class Home
{
    private List<User> DapperUsers { get; set; } = [];

    private List<User> EfCoreUsers { get; set; } = [];

    private InputText InputTextName { get; set; } = default!;

    private string Name { get; set; } = string.Empty;

    private string Email { get; set; } = string.Empty;

    private async Task Refresh(bool clearNames = false)
    {
        DapperUsers = await UserRepository.GetAllUsers();
        EfCoreUsers = await AppDbContext.Users.ToListAsync();

        if (clearNames)
        {
            Name = Email = string.Empty;
            if (InputTextName is { Element: not null })
            {
                await InputTextName.Element.Value.FocusAsync();
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await Refresh();
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

        await Refresh(clearNames: true);
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

        await Refresh(clearNames: true);
    }

    private async Task DeleteUserEfCore(int userId)
    {
        var user = await AppDbContext.Users.FindAsync(userId);

        if (user is not null)
        {
            AppDbContext.Users.Remove(user);
            await AppDbContext.SaveChangesAsync();
        }

        await Refresh();
    }

    private async Task DeleteUserDapper(int userId)
    {
        await UserRepository.DeleteUser(userId);
        await Refresh();
    }
}
