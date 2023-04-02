using Microsoft.EntityFrameworkCore;
using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.DataAccess;

public class AppDbContext : DbContext
{
  public DbSet<YerbaMateProduct> YerbaMateProducts { get; set; }
  public DbSet<BombillaProduct> BombillaProducts { get; set; }
  public DbSet<Country> Countries { get; set; }
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // new AddressConfiguration().Configure(modelBuilder.Entity<Address>());
    modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
  }
}