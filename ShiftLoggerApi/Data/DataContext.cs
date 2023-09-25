using Microsoft.EntityFrameworkCore;
using ShiftLoggerApi.Models;

namespace ShiftLoggerApi.Data;

public class DataContext : DbContext
{
    private static readonly IConfigurationRoot Config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddUserSecrets<Program>()
        .Build();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
    
        optionsBuilder.UseSqlServer(Config.GetConnectionString("ShiftsLogger"));
    
   
    
    public DbSet<Shift> Shifts { get; set; } = null!;
    public DbSet<Worker> Workers { get; set; } = null!;
}