using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public class Bombilla
{
  public int Id { get; set; }
  [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string Name { get; set; }
  [StringLength(10, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string Length { get; set; }
  [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string Material { get; set; }
  [StringLength(300, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
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
