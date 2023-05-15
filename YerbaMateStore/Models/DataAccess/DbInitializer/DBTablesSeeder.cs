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
      if (!_dbContext.Images.Any())
      {
        _dbContext.Images.AddRange(GetImages());
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

  private IEnumerable<Image> GetImages()
  {
    IEnumerable<Image> ImageList = new List<Image>()
    {
      new YerbaMateImage(){ProductId=1,ImageUrl="/images/products/YerbaMate-1/daef074b-d47c-45ed-8e49-753cca091818.png"},
      new YerbaMateImage(){ProductId=1,ImageUrl="/images/products/YerbaMate-1/58941a1e-69fa-412b-9589-538f072c3aa5.png"},
      new YerbaMateImage(){ProductId=3,ImageUrl="/images/products/YerbaMate-3/cff6c47c-8e58-42a9-8bd0-8b47b5bb6c1d.png"},
      new YerbaMateImage(){ProductId=3,ImageUrl="/images/products/YerbaMate-3/4cfd97e7-3c41-4851-b013-ec7934c6ec7c.png"},
      new YerbaMateImage(){ProductId=4,ImageUrl="/images/products/YerbaMate-4/7dbd0c95-38fe-48b3-9e16-45eb92bfddef.png"},
      new YerbaMateImage(){ProductId=5,ImageUrl="/images/products/YerbaMate-5/a0f34979-4ca8-4a36-b565-ea4196047d01.png"},
      new YerbaMateImage(){ProductId=5,ImageUrl="/images/products/YerbaMate-5/798b5fa0-2b0d-4b98-a4b6-f1167a842944.png"},
      new YerbaMateImage(){ProductId=7,ImageUrl="/images/products/YerbaMate-7/550d48a6-2573-48e1-ba3e-a114958805da.png"},
      new YerbaMateImage(){ProductId=8,ImageUrl="/images/products/YerbaMate-8/09c10271-92fe-475c-b1d0-6052e0bc303e.png"},
      new YerbaMateImage(){ProductId=6,ImageUrl="/images/products/YerbaMate-6/00254fde-9d36-4c43-9875-971dfbb8a172.png"},
      new YerbaMateImage(){ProductId=2,ImageUrl="/images/products/YerbaMate-2/0b0eb485-f429-472c-b4cc-52a82f574ff4.png"},
      new YerbaMateImage(){ProductId=2,ImageUrl="/images/products/YerbaMate-2/c0bdebb0-38b7-48e5-b825-afd254bb3422.png"},
      new BombillaImage(){ProductId=1,ImageUrl="/images/products/Bombilla-1/969312b7-53e8-4f00-b7b5-691a01608e29.png"},
      new BombillaImage(){ProductId=2,ImageUrl="/images/products/Bombilla-2/7db8beed-9b08-4772-878c-9ebbc919aea0.png"},
      new BombillaImage(){ProductId=3,ImageUrl="/images/products/Bombilla-3/0b5b0ea5-1543-457a-912e-121f9eba9fd6.png"},
      new CupImage(){ProductId=1,ImageUrl="/images/products/Cup-1/9e584713-20f8-4819-b577-c522be512388.png"},
      new CupImage(){ProductId=1,ImageUrl="/images/products/Cup-1/c1d43369-220e-4581-b9f2-14289d207b06.png"},
      new CupImage(){ProductId=2,ImageUrl="/images/products/Cup-2/5fb2bc25-2451-41fc-8557-1ed441f0a2ac.png"},
      new CupImage(){ProductId=3,ImageUrl="/images/products/Cup-3/b2fe922a-c3d5-406b-aa9a-d157def92a1f.png"}
     };
    return ImageList;
  }
}
