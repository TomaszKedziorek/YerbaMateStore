namespace YerbaMateStore.Models.Pagination;
public class YerbaMateQuery
{
  public string? type { get; set; }
  public string? country { get; set; }
  public string? brand { get; set; }
  public string? weight { get; set; }
  public int PageNumber { get; set; }
  public int PageSize { get; set; }
}
