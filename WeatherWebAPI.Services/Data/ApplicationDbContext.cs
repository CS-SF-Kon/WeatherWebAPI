using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWebAPI.Contracts.Models;

namespace WeatherWebAPI.Services.Data;

/// <summary>
/// Database context
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

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
