
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public class YerbaMate
{
  public int Id { get; set; }
  [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string Name { get; set; }
  [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string Brand { get; set; }
  [StringLength(300, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string Description { get; set; }
  [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string Composition { get; set; }
  [Display(Name = "Has Additives?")]
  public bool HasAdditives { get; set; }
  [StringLength(10, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string Weight { get; set; }
  [Range(0.01, 1000000)]
  public double Price { get; set; }
  [Display(Name = "Discount Price")]
  [Range(0.01, 1000000)]
  public double? DiscountPrice { get; set; }

  [Display(Name = "Country")]
  public int CountryId { get; set; }
  [ValidateNever]
  public Country Country { get; set; }
  [ValidateNever]
  public List<YerbaMateImage> Images { get; set; }
}
