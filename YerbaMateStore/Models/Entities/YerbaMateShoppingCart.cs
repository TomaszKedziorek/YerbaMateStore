using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public class YerbaMateShoppingCart : ShoppingCart
{
  [Column("YerbaMateProductId")]
  public int ProductId { get; set; }
  [ValidateNever]
  [ForeignKey("ProductId")]
  public YerbaMate Product { get; set; }

  public YerbaMateShoppingCart()
  {
  }
  
  public YerbaMateShoppingCart(YerbaMate product, int productId)
  {
    ProductId = productId;
    Product = product;
  }
}
