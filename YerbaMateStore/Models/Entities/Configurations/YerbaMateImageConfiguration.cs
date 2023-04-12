using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YerbaMateStore.Models.Entities.Configurations;
public class YerbaMateImageConfiguration : IEntityTypeConfiguration<YerbaMateImage>
{
  public void Configure(EntityTypeBuilder<YerbaMateImage> builder)
  {
    builder.Property(i => i.ImageUrl).IsRequired();
  }
}
