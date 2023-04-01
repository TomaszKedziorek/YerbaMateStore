using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YerbaMateStore.Models.Entities.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
  public void Configure(EntityTypeBuilder<Country> builder)
  {
    builder.Property(c => c.Name).IsRequired().HasMaxLength(30);
  }
}
