using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.DataAccess;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
  public DbSet<YerbaMate> YerbaMate { get; set; }
  public DbSet<Bombilla> Bombilla { get; set; }
  public DbSet<Cup> Cup { get; set; }
  public DbSet<Country> Countries { get; set; }
  public DbSet<YerbaMateImage> YerbaMateImages { get; set; }
  public DbSet<BombillaImage> BombillaImages { get; set; }
  public DbSet<CupImage> CupImages { get; set; }
  public DbSet<ApplicationUser> ApplicationUsers { get; set; }

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
  }
}