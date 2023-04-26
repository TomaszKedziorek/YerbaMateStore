using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public class CupShoppingCart : ShoppingCart
{
  [Column("CupProductId")]
  public int ProductId { get; set; }
  [ValidateNever]
  [ForeignKey("ProductId")]
  public Cup Product { get; set; }

  public CupShoppingCart()
  {
  }

  public CupShoppingCart(Cup product, int productId)
  {
    ProductId = productId;
    Product = product;
  }
}
