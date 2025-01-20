using System.Net;
using System.Net.Http.Json;

namespace BigChange.ToDo.ApiTests;

public class AddToDoItemTests(ToDoApplicationWebFactory factory): ToDoApplicationTestBase(factory)
{
    const string baseUrl = "api/ToDoItems";

    [Fact]
    public async Task AddToDoItems_ReturnsCreated()
    {
        var response = await Client.PostAsJsonAsync(baseUrl, new ToDoItem { Name = "Testing" });

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}