namespace BigChange.ToDo;

public sealed class ToDoItem
{
    public long Id { get; init; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}