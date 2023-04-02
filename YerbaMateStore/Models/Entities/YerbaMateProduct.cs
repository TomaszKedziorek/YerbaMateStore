
namespace YerbaMateStore.Models.Entities;

public class YerbaMateProduct
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Brand { get; set; }
  public string Description { get; set; }
  public string Composition { get; set; }
  public bool HasAdditives { get; set; }
  public string Weight { get; set; }
  public double Price { get; set; }
  public double? DiscountPrice { get; set; }

  public int CountryId { get; set; }
  public Country Country { get; set; }

  public List<YerbaMateImage> Images { get; set; } = new List<YerbaMateImage>();
}
