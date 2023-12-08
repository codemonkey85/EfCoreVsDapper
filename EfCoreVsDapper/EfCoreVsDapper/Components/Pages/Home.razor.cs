namespace EfCoreVsDapper.Components.Pages;

public partial class Home
{
    List<User> DapperUsers = [];
    List<User> EfCoreUsers = [];

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        DapperUsers = (await DapperService.GetUsersAsync()).ToList();
        EfCoreUsers = await AppDbContext.Users.ToListAsync();
    }
}
