using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public class BombillaShoppingCart : ShoppingCart
{
  [Column("BombillaProductId")]
  public int ProductId { get; set; }
  [ValidateNever]
  [ForeignKey("ProductId")]
  public Bombilla Product { get; set; }

  public BombillaShoppingCart()
  {
  }

  public BombillaShoppingCart(Bombilla product, int productId)
  {
    ProductId = productId;
    Product = product;
  }
}
