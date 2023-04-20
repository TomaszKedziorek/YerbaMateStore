using Microsoft.EntityFrameworkCore;
using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Repository;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using YerbaMateStore.Models.DataAccess.DbInitializer;

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
    builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<AppDbContext>();
    builder.Services.AddScoped<CountrySeeder>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IDbInitializer, DbInitializer>();
    builder.Services.AddSingleton<IEmailSender, EmailSender>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
      app.UseExceptionHandler("/Home/Error");
      // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
      app.UseHsts();
    }
    IServiceScope scope = app.Services.CreateScope();
    CountrySeeder countrySeeder = scope.ServiceProvider.GetRequiredService<CountrySeeder>();
    countrySeeder.Seed();

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseAuthentication();
    app.UseRouting();
    SeedDatabase(app);

    app.UseAuthorization();
    app.MapRazorPages();
    app.MapControllerRoute(
        name: "default",
        pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

    app.Run();
  }

  static void SeedDatabase(WebApplication app)
  {
    using (var scope = app.Services.CreateScope())
    {
      var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
      dbInitializer.Initialize();
    }
  }
}
