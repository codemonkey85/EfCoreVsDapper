var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

services.AddDbContext<AppDbContext>(
        options => options.UseSqlServer("name=SqlServer"));

services
    .AddSingleton<AppDapperContext>()
    .AddScoped<IUserRepository, UserRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app
    .MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(EfCoreVsDapper.Client._Imports).Assembly);

using var databaseContext =
    app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
databaseContext.Database.Migrate();

app.Run();
