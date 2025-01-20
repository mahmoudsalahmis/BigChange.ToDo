using System.Net;

namespace BigChange.ToDo.ApiTests;

public class GetToDoItemByIdTests(ToDoApplicationWebFactory factory): ToDoApplicationTestBase(factory)
{
    const string BaseUrl = "api/ToDoItems";

    [Fact]
    public async Task GetToDoItemById_ReturnsOk()
    {
        // Arrange
        var item = Seeder.ToDoItems.First();

        // Act
        var response = await Client.GetAsync($"{BaseUrl}/{item.Id}");
        
        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetToDoItemById_Return404()
    {
        // Arrange
        var invalidId = Seeder.ToDoItems.Max(a => a.Id) + 1;
        
        // Act
        var response = await Client.GetAsync($"{BaseUrl}/{invalidId}");

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}