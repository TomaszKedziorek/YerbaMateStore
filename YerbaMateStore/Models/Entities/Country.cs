namespace YerbaMateStore.Models.Entities;

public class Country
{
  public int Id { get; set; }
  public string Name { get; set; }

  public List<YerbaMateProduct>? Products { get; set; } = new List<YerbaMateProduct>();
}
