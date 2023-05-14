namespace YerbaMateStore.Models.ViewModels;

public class CartButtonViewModel
{
  public int TotalPriceForProductsInCart { get; set; }
  public string AllProductsInCartCount { get; set; }

  public CartButtonViewModel()
  {
  }

  public CartButtonViewModel(int totalPriceForProductsInCart, string allProductsInCartCount)
  {
    TotalPriceForProductsInCart = totalPriceForProductsInCart;
    AllProductsInCartCount = allProductsInCartCount;
  }

}


