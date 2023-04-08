
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public class YerbaMate
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Brand { get; set; }
  public string Description { get; set; }
  public string Composition { get; set; }
  [Display(Name = "Has Additives?")]
  public bool HasAdditives { get; set; }
  public string Weight { get; set; }
  public double Price { get; set; }
  [Display(Name = "Discount Price")]
  public double? DiscountPrice { get; set; }

  [Display(Name = "Country")]
  public int CountryId { get; set; }
  [ValidateNever]
  public Country Country { get; set; }

  public List<YerbaMateImage> Images { get; set; } = new List<YerbaMateImage>();
}
