namespace YerbaMateStore.Models.Repository.IRepository;

public interface IUnitOfWork
{

  ICountryRepository Countries { get; }
  IYerbaMateRepository YerbaMate { get; }
  void Save();
}
