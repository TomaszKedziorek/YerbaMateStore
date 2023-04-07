using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YerbaMateStore.Models.Entities.Configurations;

public class BombillaConfiguration : IEntityTypeConfiguration<Bombilla>
{
  public void Configure(EntityTypeBuilder<Bombilla> builder)
  {
    builder.Property(p => p.Name).IsRequired().HasMaxLength(30);
    builder.Property(p => p.Description).IsRequired().HasMaxLength(300);
    builder.Property(p => p.Material).IsRequired().HasMaxLength(30);
    builder.Property(p => p.IsUnscrewed).IsRequired();
    builder.Property(p => p.Length).IsRequired().HasMaxLength(10);
    builder.Property(p => p.Price).IsRequired().HasPrecision(10, 2);
    builder.Property(p => p.DiscountPrice).HasPrecision(10, 2);
    builder.HasOne(c => c.Country).WithMany(p => p.Bombilla).HasForeignKey(c => c.CountryId)
           .OnDelete(DeleteBehavior.Restrict); ;

    builder.HasMany(i => i.Images).WithOne(i => i.Product).HasForeignKey(i => i.ProductId);
  }
}