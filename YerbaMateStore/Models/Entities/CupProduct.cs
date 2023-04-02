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
  public Country Country { get; set; }

  public List<Image> Images { get; set; } = new List<Image>();
}
