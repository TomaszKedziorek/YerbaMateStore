using System.ComponentModel.DataAnnotations;

namespace YerbaMateStore.Models.Entities;

public class Country
{
  public int Id { get; set; }
  public string Name { get; set; }
  [RegularExpression(@"^[a-z]+$", ErrorMessage = "Use small letters only please!")]
  public string? CountryIsoAlfa2Code { get; set; }

  public List<YerbaMateProduct>? YerbaMateProducts { get; set; } = new List<YerbaMateProduct>();
  public List<BombillaProduct>? BombillaProducts { get; set; } = new List<BombillaProduct>();
  public List<CupProduct>? CupProducts { get; set; } = new List<CupProduct>();
}
