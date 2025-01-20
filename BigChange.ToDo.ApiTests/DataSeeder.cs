using Bogus;

namespace BigChange.ToDo.ApiTests;

public class DataSeeder(ToDoDbContext context)
{
    public List<ToDoItem> ToDoItems { get; } = [];

    public async Task SeedAsync()
    {
        var toDoFaker = new Faker<ToDoItem>()
            .RuleFor(a => a.Id, 0)
            .RuleFor(a => a.Name, f => f.Lorem.Sentence())
            .RuleFor(a => a.IsComplete, f => f.Random.Bool());
        
        ToDoItems.AddRange(toDoFaker.Generate(100));
        
        context.ToDoItems.AddRange(ToDoItems);

        await context.SaveChangesAsync();
    }
}