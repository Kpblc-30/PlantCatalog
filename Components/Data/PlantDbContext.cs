using Microsoft.EntityFrameworkCore;
using PlantCatalog.Components.Models;
namespace PlantCatalog.Components.Data;
public class PlantDbContext : DbContext
{
    public PlantDbContext(DbContextOptions<PlantDbContext> options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users => Set<User>();
}