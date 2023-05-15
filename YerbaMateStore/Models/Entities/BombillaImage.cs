using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public class BombillaImage : Image
{
  [Column("BombillaProductId")]
  public int ProductId { get; set; }
  [ValidateNever]
  public Bombilla Product { get; set; }
}
