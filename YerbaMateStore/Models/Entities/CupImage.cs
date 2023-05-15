using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public class CupImage : Image
{
  [Column("CupProductId")]
  public int ProductId { get; set; }
  [ValidateNever]
  public Cup Product { get; set; }
}
