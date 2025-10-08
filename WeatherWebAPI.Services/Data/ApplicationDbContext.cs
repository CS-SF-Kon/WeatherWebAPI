using Microsoft.EntityFrameworkCore;
using WeatherWebAPI.Contracts.Models;

namespace WeatherWebAPI.Services.Data;

/// <summary>
/// Database context
/// </summary>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<LogEntry> Logs { get; set; } // table name

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogEntry>(entity =>
        {
            entity.HasIndex(e => e.RequestTime);
            entity.HasIndex(e => e.ClientIP);
            entity.HasIndex(e => e.CityRequested);
        });
    }
}
