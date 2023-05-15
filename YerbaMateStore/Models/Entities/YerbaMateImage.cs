using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public class YerbaMateImage : Image
{
  [Column("YerbaMateProductId")]
  public int ProductId { get; set; }
  [ValidateNever]
  public YerbaMate Product { get; set; }
}
