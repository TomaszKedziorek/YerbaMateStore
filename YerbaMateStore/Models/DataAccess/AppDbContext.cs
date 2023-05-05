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
  public DbSet<ShoppingCart> ShoppingCart { get; set; }
  public DbSet<YerbaMateShoppingCart> YerbaMateShoppingCart { get; set; }
  public DbSet<BombillaShoppingCart> BombillaShoppingCart { get; set; }
  public DbSet<CupShoppingCart> CupShoppingCart { get; set; }
  public DbSet<OrderHeader> OrderHeader { get; set; }
  public DbSet<OrderDetail> OrderDetail { get; set; }
  public DbSet<YerbaMateOrderDetail> YerbaMateOrderDetail { get; set; }
  public DbSet<BombillaOrderDetail> BombillaOrderDetail { get; set; }
  public DbSet<CupOrderDetail> CupOrderDetail { get; set; }
  public DbSet<DeliveryMethod> DeliveryMethod { get; set; }
  public DbSet<PaymentMethod> PaymentMethod { get; set; }

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
  }
}
