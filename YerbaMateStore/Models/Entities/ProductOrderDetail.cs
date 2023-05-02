using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;
public class ProductOrderDetail<T> : OrderDetail where T : class, new()
{
  [Required]
  public int ProductId { get; set; }
  [ForeignKey("ProductId")]
  [ValidateNever]
  public T Product { get; set; }
}
