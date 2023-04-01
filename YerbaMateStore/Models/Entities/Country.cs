using System.ComponentModel.DataAnnotations;

namespace YerbaMateStore.Models.Entities;

public class Country
{
  public int Id { get; set; }
  public string Name { get; set; }
  [RegularExpression(@"^[a-z]+$", ErrorMessage = "Use small letters only please!")]
  public string? CountryIsoAlfa2Code { get; set; }

  public List<YerbaMateProduct>? Products { get; set; } = new List<YerbaMateProduct>();
}
