using BigChange.ToDo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("toDos")!));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //For simplicity creating the migration
    using var scope = app.Services.CreateScope();
    using var dbContext = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

const string baseUrl = "api/ToDoItems";

app.MapGet(baseUrl, (ToDoDbContext db) => db.ToDoItems.ToListAsync())
    .WithName("GetToDoList")
    .WithOpenApi();


app.MapGet("{baseUrl}/{id}", async (ToDoDbContext db, long id) =>
    {
        var todoItem = await db.ToDoItems.FindAsync(id);

        if (todoItem == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(todoItem);
    })
    .WithName("GetToDoItem")
    .WithOpenApi();

app.MapPut("{baseUrl}/{id}", async (ToDoDbContext db,[FromRoute] long id,[FromBody] ToDoItem item) =>
{
    if (id != item.Id)
    {
        return Results.BadRequest();
    }

    var todoItem = await db.ToDoItems.FindAsync(id);

    if (todoItem == null)
    {
        return Results.NotFound();
    }

    todoItem.Name = item.Name;
    todoItem.IsComplete = item.IsComplete;

    await db.SaveChangesAsync();


    return Results.NoContent();
});

app.MapDelete("{baseUrl}/{id}", async (ToDoDbContext db, long id) =>
{
    var todoItem = await db.ToDoItems.FindAsync(id);

    if (todoItem == null)
    {
        return Results.NotFound();
    }

    db.ToDoItems.Remove(todoItem);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPost(baseUrl, async ([FromBody] ToDoItem item, ToDoDbContext db) =>
    {
        db.ToDoItems.Add(item);
        await db.SaveChangesAsync();
        return Results.Created(
            $"api/ToDoItems/{item.Id}",
            item);
    })
    .WithName("AddToDoItem")
    .WithOpenApi();


app.Run();