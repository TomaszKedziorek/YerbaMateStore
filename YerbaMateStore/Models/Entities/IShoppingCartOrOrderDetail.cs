namespace YerbaMateStore.Models.Entities;

public interface IShoppingCartOrOrderDetail
{
  int Id { get; set; }

  int Quantity { get; set; }
  double Price { get; set; }
}

