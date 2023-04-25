using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YerbaMateStore.Models.Entities.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
  public void Configure(EntityTypeBuilder<ApplicationUser> builder)
  {
    builder.Property(p => p.Name).IsRequired().HasMaxLength(250);
    builder.Property(p => p.StreetAddress).HasMaxLength(250);
    builder.Property(p => p.Country).HasMaxLength(250);
    builder.Property(p => p.City).HasMaxLength(250);
    builder.Property(p => p.State).HasMaxLength(250);
    builder.Property(p => p.PostalCode).HasMaxLength(10);
  }
}
