using KorpAPI.Data;
using KorpAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KorpAPI.Routes;

public static class Tasks
{
    public static List<CustomTask> TaskList = new()
    {
        new CustomTask(1, "Do That Thing!", "Do the thing and do the other thing."),
        new CustomTask(2, "Do The Other Thing!", "Do the thing and another thing."),
        new CustomTask(3, "Do not do this Thing!", "Do first the another thing and later the thing.")
    };
    public static void MapTasksRoutes(this WebApplication app)
    {
        app.MapGet("/tasks", async (AppDbContext context) =>
        {
            var allTasks = await context.Tasks.ToListAsync();
            return allTasks;
        });
        
        app.MapGet("/tasks/{title}", async (string title, AppDbContext context) =>
        {
            var allTasks = await context.Tasks.ToListAsync();
            return allTasks.FindAll(x => x.Title.StartsWith(title, StringComparison.OrdinalIgnoreCase));
        });

        app.MapPost("/tasks/", async (CustomTask task, AppDbContext context) =>
        {
            var allTasks = await context.Tasks.ToListAsync();

            if (allTasks.Any(t => string.Equals(t.Title, task.Title, StringComparison.OrdinalIgnoreCase)))
            {
                return Results.BadRequest(new { message = "Task with the same title already exists." });
            }

            task.Id = allTasks.Count == 0 ? 0 : allTasks.Max(t => t.Id) + 1;
            
            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();

            return Results.Ok(task);
        });

        app.MapPut("/tasks/{id:int}", async (int id, CustomTask updatedTask, AppDbContext context) =>
        {
            var found = await context.Tasks.FindAsync(id);

            if (found == null)
                return Results.NotFound();

            found.Title = updatedTask.Title;
            found.Description = updatedTask.Description;

            await context.SaveChangesAsync();

            return Results.Ok(found);
        });
        
        app.MapDelete("/tasks/{id:int}", async (int id, AppDbContext context) =>
        {
            var found = await context.Tasks.FindAsync(id);

            if (found == null)
                return Results.NotFound();

            context.Tasks.Remove(found);
            await context.SaveChangesAsync();

            return Results.Ok(found);
        });
    }
}