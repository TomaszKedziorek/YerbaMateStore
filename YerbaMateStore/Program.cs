using Microsoft.EntityFrameworkCore;
using YerbaMateStore.Models.DataAccess;

namespace YerbaMateStore;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();
    //Allow Razor Pages
    var mvcBuilder = builder.Services.AddRazorPages();
    //Allow refresz cshtml,css,js at runtime
    if (builder.Environment.IsDevelopment())
      mvcBuilder.AddRazorRuntimeCompilation();

    string connectionString = builder.Configuration.GetConnectionString("YerbaMateStoreDbConnsectionString");
    ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
    builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, serverVersion));

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
      app.UseExceptionHandler("/Home/Error");
      // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
      app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

    app.Run();
  }
}