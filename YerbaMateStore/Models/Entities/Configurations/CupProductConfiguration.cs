using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YerbaMateStore.Models.Entities.Configurations;

public class CupProductConfiguration : IEntityTypeConfiguration<CupProduct>
{
  public void Configure(EntityTypeBuilder<CupProduct> builder)
  {
    builder.Property(p => p.Name).IsRequired().HasMaxLength(30);
    builder.Property(p => p.Description).IsRequired().HasMaxLength(300);
    builder.Property(p => p.Material).IsRequired().HasMaxLength(30);
    builder.Property(p => p.Volume).IsRequired().HasMaxLength(20);
    builder.Property(p => p.Price).IsRequired().HasPrecision(10, 2);
    builder.Property(p => p.DiscountPrice).HasPrecision(10, 2);
    builder.HasOne(c => c.Country).WithMany(p => p.CupProducts).HasForeignKey(c => c.CountryId)
           .OnDelete(DeleteBehavior.Restrict);

    builder.HasMany(i => i.Images).WithOne().HasForeignKey(i => i.ProductId);
  }
}
