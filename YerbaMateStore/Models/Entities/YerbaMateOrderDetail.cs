using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;
public class YerbaMateOrderDetail : OrderDetail 
{
  [Required,Column("YerbaMateProductId")]
  public int ProductId { get; set; }
  [ForeignKey("ProductId")]
  [ValidateNever]
  public YerbaMate Product { get; set; }
}
