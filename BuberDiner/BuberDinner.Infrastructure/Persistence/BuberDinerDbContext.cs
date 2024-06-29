using BuberDinner.Domain.Menu;
using BuberDinner.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace BuberDinner.Infrastructure.Persistence;

public class BuberDinerDbContext : DbContext
{
    public BuberDinerDbContext(DbContextOptions<BuberDinerDbContext> options) : base(options)
    {
    }

    public DbSet<Menu> Menus { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BuberDinerDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}