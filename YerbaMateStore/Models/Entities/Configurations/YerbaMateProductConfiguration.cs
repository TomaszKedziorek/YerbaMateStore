using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YerbaMateStore.Models.Entities.Configurations;

public class YerbaMateProductConfiguration : IEntityTypeConfiguration<YerbaMateProduct>
{
  public void Configure(EntityTypeBuilder<YerbaMateProduct> builder)
  {
    builder.Property(p => p.Name).IsRequired().HasMaxLength(30);
    builder.Property(p => p.Brand).IsRequired().HasMaxLength(30);
    builder.Property(p => p.Description).IsRequired().HasMaxLength(300);
    builder.Property(p => p.Additives).IsRequired();
    builder.Property(p => p.Weight).IsRequired().HasMaxLength(10);
    builder.Property(p => p.Price).IsRequired().HasPrecision(10, 2);
    builder.Property(p => p.DiscountPrice).HasPrecision(10, 2);
    builder.HasOne(c => c.Country).WithMany(p => p.Products).HasForeignKey(c => c.CountryId);
  }
}
