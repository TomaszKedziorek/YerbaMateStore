using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public class Cup
{
  public int Id { get; set; }
  [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string Name { get; set; }
  [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string Volume { get; set; }
  [StringLength(20, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string Material { get; set; }
  [StringLength(300, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string Description { get; set; }
  [Range(0.01, 1000000)]
  public double Price { get; set; }
  [Range(0.01, 1000000)]
  [Display(Name = "Discount Price")]
  public double? DiscountPrice { get; set; }

  [ValidateNever]
  public List<CupImage> Images { get; set; }
}