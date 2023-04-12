using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.ViewModels;

public class YerbaMateViewModel
{
  private readonly IUnitOfWork _unitOfWork;
  public YerbaMate YerbaMate { get; set; }

  [ValidateNever]
  public IEnumerable<SelectListItem> CountryList { get; set; }
  public YerbaMateViewModel()
  {
  }

  public YerbaMateViewModel(IUnitOfWork unitOfWork, YerbaMate? product = null, int? id = null)
  {
    _unitOfWork = unitOfWork;
    this.CountryList = _unitOfWork.Countries.GetAll().Select(c => new SelectListItem
    {
      Text = c.Name,
      Value = c.Id.ToString(),
    });
    if (id == null || id == 0)
    {
      this.YerbaMate = product == null ? new YerbaMate() : product;
    }
    else
    {
      this.YerbaMate = _unitOfWork.YerbaMate.GetFirstOrDefault(p => p.Id == id, includeProperties: "Images");
    }
  }
}
