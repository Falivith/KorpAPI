using KorpAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KorpAPI.Data;

public class AppDbContext : DbContext
{
    public DbSet<CustomTask> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Banco.sqlite");
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        optionsBuilder.EnableSensitiveDataLogging();
        
        base.OnConfiguring(optionsBuilder);
    }
}
