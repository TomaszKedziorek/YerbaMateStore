using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.Repository.IRepository;

public interface IApplicationUserRepository : IRepository<ApplicationUser>
{
  void Update(ApplicationUser applicationUser);
}