using Microsoft.EntityFrameworkCore;
using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.DataAccess;

public class AppDbContext : DbContext
{

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }
}