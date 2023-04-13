using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Models.Managers;

public class ProductManager<T> where T : class, new()
{
  private readonly string wwwRootPath;
  private ProductViewModel<T>? ProductVM { get; set; }

  public ProductManager(IWebHostEnvironment hostEnvironment, ProductViewModel<T>? productVM = null)
  {
    wwwRootPath = hostEnvironment.WebRootPath;
    ProductVM = productVM;
  }

  public void SaveFile(ref Image<T> productImage, IFormFile file)
  {
    string extension = Path.GetExtension(file.FileName);
    string fileName = Guid.NewGuid().ToString();
    char separation = Path.DirectorySeparatorChar;
    var productDirectoryName = typeof(T).Name;
    var Id = ProductVM.Product.GetType().GetProperty("Id").GetValue(ProductVM.Product);
    string productPath = @$"images{separation}products{separation}{productDirectoryName}-{Id}";
    string finalPath = Path.Combine(wwwRootPath, productPath);
    if (!Directory.Exists(finalPath))
      Directory.CreateDirectory(finalPath);

    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName + extension), FileMode.Create))
    {
      file.CopyTo(fileStream);
    }

    productImage.ProductId = Convert.ToInt32(Id);
    productImage.ImageUrl = @$"{separation}{productPath}{separation}{fileName}{extension}";
  }

  public void DeleteFile(Image<T> imageToDelete)
  {
    if (!string.IsNullOrEmpty(imageToDelete.ImageUrl))
    {
      string productImageDbPath = imageToDelete.ImageUrl.Trim('/');
      string imagePath = Path.Combine(wwwRootPath, productImageDbPath);
      if (System.IO.File.Exists(imagePath))
      {
        System.IO.File.Delete(imagePath);
      }
    }
  }
}

