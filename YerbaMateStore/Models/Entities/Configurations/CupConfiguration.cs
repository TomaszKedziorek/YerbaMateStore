using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YerbaMateStore.Models.Entities.Configurations;

public class CupConfiguration : IEntityTypeConfiguration<Cup>
{
  public void Configure(EntityTypeBuilder<Cup> builder)
  {
    builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
    builder.Property(p => p.Description).IsRequired().HasMaxLength(300);
    builder.Property(p => p.Material).IsRequired().HasMaxLength(50);
    builder.Property(p => p.Volume).IsRequired().HasMaxLength(20);
    builder.Property(p => p.Price).IsRequired().HasPrecision(10, 2);
    builder.Property(p => p.DiscountPrice).HasPrecision(10, 2);

    builder.HasMany(i => i.Images).WithOne(i => i.Product).HasForeignKey(i => i.ProductId);
  }
}
