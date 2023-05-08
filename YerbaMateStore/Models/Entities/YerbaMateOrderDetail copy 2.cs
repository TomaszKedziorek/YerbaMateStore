using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;
public class CupOrderDetail : OrderDetail 
{
  [Required,Column("CupProductId")]
  public int ProductId { get; set; }
  [ForeignKey("ProductId")]
  [ValidateNever]
  public Cup Product { get; set; }
}
