using Microsoft.Extensions.DependencyInjection;

namespace BigChange.ToDo.ApiTests;

public class ToDoApplicationTestBase : IClassFixture<ToDoApplicationWebFactory>, IAsyncLifetime
{
    protected HttpClient Client { get; }
    protected DataSeeder Seeder { get; }

    private readonly ToDoDbContext _dbContext;
    protected ToDoApplicationTestBase(ToDoApplicationWebFactory factory)
    {
        Client = factory.CreateClient();

        var scope = factory.Services.CreateScope();
        _dbContext = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
        Seeder = new DataSeeder(_dbContext);
        
    }

    public async Task InitializeAsync()
    {
        await Seeder.SeedAsync();
    }

    public Task DisposeAsync()
    {
        return _dbContext.DisposeAsync().AsTask();
    }
}