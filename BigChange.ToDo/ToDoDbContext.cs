using Microsoft.EntityFrameworkCore;

namespace BigChange.ToDo;

public class ToDoDbContext(DbContextOptions<ToDoDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<ToDoItem> ToDoItems { get; set; } = null!;
}