using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public class CupProduct
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Volume { get; set; }
  public string Material { get; set; }
  public string Description { get; set; }
  public double Price { get; set; }
  public double? DiscountPrice { get; set; }

  public int CountryId { get; set; }
  [ValidateNever]
  public Country Country { get; set; }

  public List<CupImage> Images { get; set; } = new List<CupImage>();
}
