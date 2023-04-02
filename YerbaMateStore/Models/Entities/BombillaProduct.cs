namespace YerbaMateStore.Models.Entities;

public class BombillaProduct
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Length { get; set; }
  public string Material { get; set; }
  public string Description { get; set; }
  public bool IsUnscrewed { get; set; }
  public double Price { get; set; }
  public double? DiscountPrice { get; set; }

  public int CountryId { get; set; }
  public Country Country { get; set; }
}
