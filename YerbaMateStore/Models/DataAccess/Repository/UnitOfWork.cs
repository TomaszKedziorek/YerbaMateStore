using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.DataAccess.Repository;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.Repository;
public class UnitOfWork : IUnitOfWork
{
  private readonly AppDbContext _dbContext;
  public IYerbaMateRepository YerbaMate { get; private set; }
  public ICountryRepository Countries { get; private set; }
  public IImageRepository<YerbaMateImage> YerbaMateImage { get; private set; }

  public IBombillaRepository Bombilla { get; private set; }

  public IImageRepository<BombillaImage> BombillaImage { get; private set; }
  public ICupRepository Cup { get; private set; }

  public IImageRepository<CupImage> CupImage { get; private set; }
  public IApplicationUserRepository ApplicationUser { get; private set; }
  public IShoppingCartRepository<YerbaMateShoppingCart> YerbaMateShoppingCart { get; private set; }
  public IShoppingCartRepository<BombillaShoppingCart> BombillaShoppingCart { get; private set; }
  public IShoppingCartRepository<CupShoppingCart> CupShoppingCart { get; private set; }
  public IShoppingCartRepository<ShoppingCart> ShoppingCart { get; private set; }
  public IOrderHeaderRepository OrderHeader { get; private set; }
  public IOrderDetailRepository<YerbaMateOrderDetail> YerbaMateOrderDetail { get; private set; }
  public IOrderDetailRepository<BombillaOrderDetail> BombillaOrderDetail { get; private set; }
  public IOrderDetailRepository<CupOrderDetail> CupOrderDetail { get; private set; }
  public IPaymentMethodRepository PaymentMethod { get; private set; }
  public IDeliveryMethodRepository DeliveryMethod { get; private set; }

  public UnitOfWork(AppDbContext dbContext)
  {
    _dbContext = dbContext;
    YerbaMate = new YerbaMateRepository(dbContext);
    Countries = new CountryRepository(dbContext);
    YerbaMateImage = new YerbaMateImageRepository(dbContext);
    Bombilla = new BombillaRepository(dbContext);
    BombillaImage = new BombillaImageRepository(dbContext);
    Cup = new CupRepository(dbContext);
    CupImage = new CupImageRepository(dbContext);
    ApplicationUser = new ApplicationUserRepository(dbContext);
    YerbaMateShoppingCart = new ShoppingCartRepository<YerbaMateShoppingCart>(dbContext);
    BombillaShoppingCart = new ShoppingCartRepository<BombillaShoppingCart>(dbContext);
    CupShoppingCart = new ShoppingCartRepository<CupShoppingCart>(dbContext);
    ShoppingCart = new ShoppingCartRepository<ShoppingCart>(dbContext);
    OrderHeader = new OrderHeaderRepository(dbContext);
    YerbaMateOrderDetail = new OrderDetailRepository<YerbaMateOrderDetail>(dbContext);
    BombillaOrderDetail = new OrderDetailRepository<BombillaOrderDetail>(dbContext);
    CupOrderDetail = new OrderDetailRepository<CupOrderDetail>(dbContext);
    PaymentMethod = new PaymentMethodRepository(dbContext);
    DeliveryMethod = new DeliveryMethodRepository(dbContext);
  }

  public void Save()
  {
    _dbContext.SaveChanges();
  }
}