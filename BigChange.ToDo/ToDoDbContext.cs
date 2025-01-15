using Microsoft.EntityFrameworkCore;

namespace BigChange.ToDo;

public class ToDoDbContext(DbContextOptions<ToDoDbContext> options) : DbContext(options)
{
    public DbSet<ToDoItem> ToDoItems { get; set; } = null!;
}