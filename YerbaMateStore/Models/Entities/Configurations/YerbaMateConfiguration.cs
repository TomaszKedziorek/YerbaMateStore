using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YerbaMateStore.Models.Entities.Configurations;

public class YerbaMateConfiguration : IEntityTypeConfiguration<YerbaMate>
{
  public void Configure(EntityTypeBuilder<YerbaMate> builder)
  {
    builder.Property(p => p.Name).IsRequired().HasMaxLength(30);
    builder.Property(p => p.Brand).IsRequired().HasMaxLength(30);
    builder.Property(p => p.Description).IsRequired().HasMaxLength(300);
    builder.Property(p => p.HasAdditives).IsRequired();
    builder.Property(p => p.Weight).IsRequired().HasMaxLength(10);
    builder.Property(p => p.Price).IsRequired().HasPrecision(10, 2);
    builder.Property(p => p.DiscountPrice).HasPrecision(10, 2);
    builder.HasOne(c => c.Country).WithMany(p => p.YerbaMate).HasForeignKey(c => c.CountryId)
           .OnDelete(DeleteBehavior.Restrict);

    builder.HasMany(i => i.Images).WithOne(i=>i.Product).HasForeignKey(i => i.ProductId);
  }
}