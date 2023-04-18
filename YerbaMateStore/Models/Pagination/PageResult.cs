using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Models.Pagination;
public class PagedResult<T> where T : IProductsViewModel
{
  public IProductsViewModel ProductsViewModel { get; set; }
  public int TotalPages { get; set; }
  public int ItemsFrom { get; set; }
  public int ItemsTo { get; set; }
  public int TotalItemsCount { get; set; }


  public PagedResult(IProductsViewModel productsViewModel, int totalCount, int pageSize, int pageNumber)
  {
    ProductsViewModel = productsViewModel;
    TotalItemsCount = totalCount;
    ItemsFrom = pageSize * (pageNumber - 1) + 1;
    ItemsTo = ItemsFrom + pageSize - 1;
    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
  }
}