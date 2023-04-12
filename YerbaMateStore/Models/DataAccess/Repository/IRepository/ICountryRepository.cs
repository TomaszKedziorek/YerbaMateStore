using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.Repository.IRepository
{
  public interface ICountryRepository : IRepository<Country>
  {
    void Update(Country country);
  }
}