namespace YerbaMateStore.Models.Repository.IRepository;

public interface IUnitOfWork
{
  IYerbaMateRepository YerbaMate { get; }
  void Save();
}
