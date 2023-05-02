using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public abstract class OrderDetail
{
  public int Id { get; set; }
  [Required]
  public int OrderId { get; set; }
  [ForeignKey("OrderId")]
  [ValidateNever]
  public OrderHeader OrderHeader { get; set; }
  public int Quantity { get; set; }
  public double Price { get; set; }
}