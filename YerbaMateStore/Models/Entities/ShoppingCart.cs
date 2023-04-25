using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public class ShoppingCart<T> where T : class, new()
{
  public int Id { get; set; }
  public int ProductId { get; set; }
  [ValidateNever]
  [ForeignKey("ProductId")]
  public T Product { get; set; }

  public ShoppingCart()
  {
  }

  public ShoppingCart(T product, int productId)
  {
    this.Product = product;
    this.ProductId = productId;
  }

}
