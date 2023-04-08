using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.Repository;

public class CountryRepository : Repository<Country>, ICountryRepository
{
  private readonly AppDbContext _dbContext;
  public CountryRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public void Update(Country country)
  {
    _dbContext.Countries.Update(country);
  }
}
