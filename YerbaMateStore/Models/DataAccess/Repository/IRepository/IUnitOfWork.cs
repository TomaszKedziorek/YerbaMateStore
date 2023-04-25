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
  void Save();
}
