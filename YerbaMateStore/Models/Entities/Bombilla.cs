using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public class Bombilla
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Length { get; set; }
  public string Material { get; set; }
  public string Description { get; set; }
  [Display(Name = "Is   Unscrewed?")]
  public bool IsUnscrewed { get; set; }
  [Range(0.01, 1000000)]
  public double Price { get; set; }
  [Range(0.01, 1000000)]
  [Display(Name = "Discount Price")]
  public double? DiscountPrice { get; set; }

  [ValidateNever]
  public List<BombillaImage> Images { get; set; }
}
