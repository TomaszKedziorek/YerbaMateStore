using System.Reflection;
using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.DataAccess.DbInitializer;

public class DBTablesSeeder
{
  private readonly AppDbContext _dbContext;

  public DBTablesSeeder(AppDbContext dbContext)
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
      if (!_dbContext.YerbaMate.Any())
      {
        _dbContext.YerbaMate.AddRange(GetYerbaMateProducts());
        _dbContext.SaveChanges();
      }
      if (!_dbContext.Bombilla.Any())
      {
        _dbContext.Bombilla.AddRange(GetBombillaProducts());
        _dbContext.SaveChanges();
      }
      if (!_dbContext.Cup.Any())
      {
        _dbContext.Cup.AddRange(GetCupProducts());
        _dbContext.SaveChanges();
      }
      if (!_dbContext.PaymentMethod.Any())
      {
        _dbContext.PaymentMethod.AddRange(GetPaymentMethod());
        _dbContext.SaveChanges();
      }
      if (!_dbContext.DeliveryMethod.Any())
      {
        _dbContext.DeliveryMethod.AddRange(GetDeliveryMethod());
        _dbContext.SaveChanges();
      }
    }
  }

  private IEnumerable<Country> GetCountries()
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

  private IEnumerable<YerbaMate> GetYerbaMateProducts()
  {
    IEnumerable<YerbaMate> YerbaMateList = new List<YerbaMate>()
      {
        new YerbaMate
        {
          Name = "Compuesta con Hierbas Menta y Boldo",
          Brand = "Kurupi",
          Description = "<p>Kurupi Menta y Boldo</p>",
          Composition = "Yerba Mate, Mint, Boldo",
          HasAdditives = true,
          Weight = "500g",
          Price = 22.5,
          DiscountPrice = null,
          CountryId = 1
        },
        new YerbaMate
        {
          Name = "Elaborada Con Palo Tradicional",
          Brand = "Rosamonte",
          Description = "<p>Rosamonte Elaborada con Palo is a traditional Argentinian yerba mate without additives with a smoky-bitter aroma. One of the most popular yerba mate on the market.</p>",
          Composition = "100% yerba mate",
          HasAdditives = false,
          Weight = "500g",
          Price = 24.45,
          DiscountPrice = 19.99,
          CountryId = 4
        },
        new YerbaMate
        {
          Name = "Elaborada Con Palo Tradicional ",
          Brand = "Pajarito",
          Description = "<p>Pajarito Elaborada Con Palo Tradicional is a traditional yerba mate from Paraguay. It has a strong smoky aroma and a distinct flavor.</p>",
          Composition = "100% yerba mate",
          HasAdditives = false,
          Weight = "1000g",
          Price = 29,
          DiscountPrice = null,
          CountryId = 1
        },
        new YerbaMate
        {
          Name = "Compuesta con Hierbas",
          Brand = "Pajarito",
          Description = "<p><strong>Pajarito Compuesta con Hierbas</strong> is a herbal yerba mate from Paraguay. It has a bitter aroma combined with a crisp addition of mint. It stimulates well.</p>",
          Composition = "Mint, burito, cedron",
          HasAdditives = true,
          Weight = "500g",
          Price = 22,
          DiscountPrice = 20,
          CountryId = 1
        },
        new YerbaMate
        {
          Name = "Menta Boldo",
          Brand = "Guarani",
          Description = "<p><strong>Guarani Boldo Menta </strong>is one of the most mate tea combinations in South America. Smoky and bitter mate tea was combined with refreshing mint and delicate boldo.</p>",
          Composition = "Yerba Mate, Mint, Boldo",
          HasAdditives = true,
          Weight = "500g",
          Price = 19,
          DiscountPrice = null,
          CountryId = 1
        },
        new YerbaMate
        {
          Name = "Mate Green Despalada",
          Brand = "Mate Green",
          Description = "<p>Mate Green Despalada is&nbsp; a delicate yerba without any additions.<br />The leaves are dried without smoke, with hot air, and short aging time makes the drought remains a green color.</p>",
          Composition = "100% Yerba Mate (Ilex paraguariensis)",
          HasAdditives = false,
          Weight = "400g",
          Price = 25,
          DiscountPrice = 23,
          CountryId = 3
        },
        new YerbaMate
        {
          Name = "Compuesta con Hierbas Menta y Boldo",
          Brand = "Kurupi",
          Description = "<p>Yerba mate kurupi</p>",
          Composition = "Yerba Mate, Mint, Boldo",
          HasAdditives = true,
          Weight = "250g",
          Price = 13.85,
          DiscountPrice = 13,
          CountryId = 1
        },
        new YerbaMate
        {
          Name = "Elaborada Con Palo Tradicional ",
          Brand = "Parajito",
          Description = "<p>Yerba</p>",
          Composition = "100% Yerba Mate (Ilex paraguariensis)",
          HasAdditives = false,
          Weight = "500g",
          Price = 19,
          DiscountPrice = 18,
          CountryId = 1
        }
      };
    return YerbaMateList;
  }

  private IEnumerable<Bombilla> GetBombillaProducts()
  {
    IEnumerable<Bombilla> BombillaList = new List<Bombilla>()
      {
        new Bombilla
        {
          Name = "Bombilla Grande - green stone",
          Description = "<p>Bombilla Grande made by INOX steel, with green cooling stone. Equipped with spoon-like filter with strainer. Allows comfortable drinking of most types of yerba mate. Producer: Cebador</p><p> With cleaning tool in package.</p>",
          Material = "INOX Steel",
          IsUnscrewed = true,
          Length = "22cm",
          Price = 30.5,
          DiscountPrice = null
        },
        new Bombilla
        {
          Name = "Bombilla Grande - red stone",
          Description =
          "<p>Bombilla Grande made by INOX steel, with red cooling stone. Equipped with spoon-like filter with strainer. Allows comfortable drinking of most types of yerba mate. Producer: Cebador</p><p> With cleaning tool in package.</p>",
          Material = "INOX Steel",
          IsUnscrewed = true,
          Length = "22cm",
          Price = 30.99,
          DiscountPrice = null
        },
        new Bombilla
        {
          Name = "Bombilla Gringo",
          Description = "<p>Simple stainless steel bombilla. Equipped with spoon-like filter with strainer. Allows comfortable drinking of most types of yerba mate. Producer: Cebador</p>",
          Material = "Stainless Steel",
          IsUnscrewed = false,
          Length = "19cm",
          Price = 19,
          DiscountPrice = 15
        }
      };
    return BombillaList;
  }

  private IEnumerable<Cup> GetCupProducts()
  {
    IEnumerable<Cup> CupList = new List<Cup>()
      {
        new Cup
        {
          Name = "Cup Palo Santo",
          Description = $"<p>This is large yerba vessel made of palo santo wood. The south america tree and it's name means a {"Holy Trunk"}  is rich in a fragrant resin with an ethereal aroma released on contact with warm water. The infusion prepared in such vessel achieve additional unique taset.</p>",
          Material = "Palo Santo wood",
          Volume = "250ml",
          Price = 68,
          DiscountPrice = null
        },
        new Cup
        {
          Name = "Goblet Palo Santo",
          Description = $"<p>Goblet shape vessel made of palo santo wood. The south america tree and it's name means a {"Holy Trunk"} is rich in a fragrant resin with an ethereal aroma released on contact with warm water. The infusion prepared in such vessel achieve additional unique taset.</p>",
          Material = "Palo Santo wood",
          Volume = "120-140ml",
          Price = 60,
          DiscountPrice = null
        },

        new Cup
        {
          Name = "Classic Gourd",
          Description = "<p>A small&nbsp; gourd with a red-brown color, obtained by thermal method. It has a metal fitting at the top and thick walls to ensure durability. <br />WARNING!!! The gourd vessel requires preparation before use, the so-called curado.</p>",
          Material = "Gourd, metal",
          Volume = "150-210ml",
          Price = 32.55,
          DiscountPrice = 28
        }
      };
    return CupList;
  }
  private IEnumerable<DeliveryMethod> GetDeliveryMethod()
  {
    IEnumerable<DeliveryMethod> DeliveryMethodList = new List<DeliveryMethod>()
    {
      new DeliveryMethod { Carrier = "Post", DeliveryTime = "3-7 days", PaymentMethodId = 1, Cost = 20},
      new DeliveryMethod { Carrier = "Post", DeliveryTime = "3-7 days", PaymentMethodId = 2, Cost = 15},
      new DeliveryMethod { Carrier = "Courier", DeliveryTime = "3-5 days", PaymentMethodId = 2, Cost = 18.5},
      new DeliveryMethod { Carrier = "Pickup in store", DeliveryTime = "1 day", PaymentMethodId = 3, Cost = 0},
      new DeliveryMethod { Carrier = "Courier", DeliveryTime = "2-5 days", PaymentMethodId = 1, Cost = 19}
    };
    return DeliveryMethodList;
  }

  private IEnumerable<PaymentMethod> GetPaymentMethod()
  {
    IEnumerable<PaymentMethod> PaymentMethodList = new List<PaymentMethod>()
    {
      new PaymentMethod(){Name="Cash on Delivery",IsTransfer = false},
      new PaymentMethod(){Name="Bank Transfer",IsTransfer = true},
      new PaymentMethod(){Name="Cash/Card",IsTransfer = false},
     };
    return PaymentMethodList;
  }
}
