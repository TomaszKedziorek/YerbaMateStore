// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;

// namespace YerbaMateStore.Models.Entities.Configurations;
// public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart<T>>
// {
//   public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ShoppingCart<T>> builder)
//   {
//     builder.HasOne(i => i.Product).HasForeignKey(i => i.ProductId);
//   }
// }