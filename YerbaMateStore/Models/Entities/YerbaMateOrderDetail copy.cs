using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;
public class BombillaOrderDetail : OrderDetail 
{
  [Required,Column("BombillaProductId")]
  public int ProductId { get; set; }
  [ForeignKey("ProductId")]
  [ValidateNever]
  public Bombilla Product { get; set; }
}
