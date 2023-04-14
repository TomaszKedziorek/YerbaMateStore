using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public class Cup
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Volume { get; set; }
  public string Material { get; set; }
  public string Description { get; set; }
  [Range(0.01, 1000000)]
  public double Price { get; set; }
  [Range(0.01, 1000000)]
  [Display(Name = "Discount Price")]
  public double? DiscountPrice { get; set; }

  [ValidateNever]
  public List<CupImage> Images { get; set; }
}