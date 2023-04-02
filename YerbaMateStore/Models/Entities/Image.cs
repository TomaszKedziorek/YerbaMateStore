namespace YerbaMateStore.Models.Entities;

public abstract class Image<T> where T : class
{
  public int Id { get; set; }
  public string Url { get; set; }
  public int ProductId { get; set; }
  public T Product { get; set; }
}
