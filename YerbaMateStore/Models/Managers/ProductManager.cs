using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Models.Managers;

public class ProductManager<T> where T : class, new()
{
  private readonly string wwwRootPath;
  private ProductViewModel<T>? ProductVM { get; set; }
  private char separationChar;
  private string productDirectoryName;

  public ProductManager(IWebHostEnvironment hostEnvironment, ProductViewModel<T>? productVM = null)
  {
    wwwRootPath = hostEnvironment.WebRootPath;
    ProductVM = productVM;
    separationChar = Path.DirectorySeparatorChar;
    productDirectoryName = typeof(T).Name;
  }

  public void SaveFile(ref Image<T> productImage, IFormFile file)
  {
    string extension = Path.GetExtension(file.FileName);
    string fileName = Guid.NewGuid().ToString() + extension;
    string? Id = ProductVM.Product.GetType().GetProperty("Id").GetValue(ProductVM.Product).ToString();
    string productPath = @$"images{separationChar}products{separationChar}{productDirectoryName}-{Id}";
    string finalPath = Path.Combine(wwwRootPath, productPath);

    if (!Directory.Exists(finalPath))
    {
      Directory.CreateDirectory(finalPath);
    }

    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
    {
      file.CopyTo(fileStream);
    }

    productImage.ProductId = Convert.ToInt32(Id);
    productImage.ImageUrl = @$"{separationChar}{productPath}{separationChar}{fileName}";
  }

  public void DeleteFiles(int? id)
  {
    string productPath = @$"images{separationChar}products{separationChar}{productDirectoryName}-{id}";
    string finalPath = Path.Combine(wwwRootPath, productPath);
    if (Directory.Exists(finalPath))
    {
      string[] filePaths = Directory.GetFiles(finalPath);
      foreach (var filePath in filePaths)
      {
        System.IO.File.Delete(filePath);
      }
      Directory.Delete(finalPath);
    }
  }

  public void DeleteFile(Image<T> imageToDelete)
  {
    if (!string.IsNullOrEmpty(imageToDelete.ImageUrl))
    {
      string productImageDbPath = imageToDelete.ImageUrl.Trim(separationChar);
      string imagePath = Path.Combine(wwwRootPath, productImageDbPath);
      if (System.IO.File.Exists(imagePath))
      {
        System.IO.File.Delete(imagePath);
      }
    }
  }
}

