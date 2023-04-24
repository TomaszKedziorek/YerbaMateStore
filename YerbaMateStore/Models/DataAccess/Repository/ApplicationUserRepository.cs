using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.Repository;

public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
{
  private readonly AppDbContext _dbContext;
  public ApplicationUserRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public void Update(ApplicationUser ApplicationUser)
  {
    _dbContext.ApplicationUsers.Update(ApplicationUser);
  }
}
