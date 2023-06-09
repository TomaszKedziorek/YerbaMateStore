using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.Repository.IRepository;

public interface IUnitOfWork
{
  ICountryRepository Countries { get; }
  IYerbaMateRepository YerbaMate { get; }
  IImageRepository<YerbaMateImage> YerbaMateImage { get; }
  IBombillaRepository Bombilla { get; }
  IImageRepository<BombillaImage> BombillaImage { get; }
  ICupRepository Cup { get; }
  IImageRepository<CupImage> CupImage { get; }
  IApplicationUserRepository ApplicationUser { get; }
  IShoppingCartRepository<YerbaMateShoppingCart> YerbaMateShoppingCart { get; }
  IShoppingCartRepository<BombillaShoppingCart> BombillaShoppingCart { get; }
  IShoppingCartRepository<CupShoppingCart> CupShoppingCart { get; }
  IShoppingCartRepository<ShoppingCart> ShoppingCart { get; }
  IOrderHeaderRepository OrderHeader { get; }
  IOrderDetailRepository<YerbaMateOrderDetail> YerbaMateOrderDetail { get; }
  IOrderDetailRepository<BombillaOrderDetail> BombillaOrderDetail { get; }
  IOrderDetailRepository<CupOrderDetail> CupOrderDetail { get; }
  IPaymentMethodRepository PaymentMethod { get; }
  IDeliveryMethodRepository DeliveryMethod { get; }

  void Save();
}
