using Reservas.Models;
using System.Data.Entity;


public class AppDbContext : DbContext
{
    public DbSet<Service> Services { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}