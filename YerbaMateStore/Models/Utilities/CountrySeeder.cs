using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.Utilities;

public class CountrySeeder
{
  private readonly AppDbContext _dbContext;

  public CountrySeeder(AppDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public void Seed()
  {
    if (_dbContext.Database.CanConnect())
    {
      if (!_dbContext.Countries.Any())
      {
        var countries = GetCountries();
        _dbContext.Countries.AddRange(countries);
        _dbContext.SaveChanges();
      }
    }
  }

  public IEnumerable<Country> GetCountries()
  {
    List<Country> countries = new List<Country>();
    string[] countryNames = { "Paragwaj", "Urugwaj", "Brazylia", "Argentyna" };
    string[] isoAlpha2Code = { "py", "uy", "br", "ar" };
    for (int i = 0; i < countryNames.Length; i++)
    {
      Country country = new Country()
      {
        Name = countryNames[i],
        CountryIsoAlfa2Code = isoAlpha2Code[i]
      };
      countries.Add(country);
    }
    return countries;
  }
}
