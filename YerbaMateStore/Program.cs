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
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultUI();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IDbInitializer, DbInitializer>();
    builder.Services.AddSingleton<IEmailSender, EmailSender>();

    AdminData adminData = builder.Configuration.GetSection("AdminData").Get<AdminData>();
    EmailSenderAccessData emailAccessData = builder.Configuration.GetSection("EmailSender").Get<EmailSenderAccessData>();
    builder.Services.AddSingleton(adminData);
    builder.Services.AddSingleton(emailAccessData);
    builder.Services.ConfigureApplicationCookie(options =>
    {
      options.LoginPath = $"/Identity/Account/Login";
      options.LogoutPath = $"/Identity/Account/Logout";
      options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    });
    builder.Services.Configure<IdentityOptions>(options =>
    {
      // Password settings.
      options.Password.RequireNonAlphanumeric = false;
      // SignIn settings.
      options.SignIn.RequireConfirmedEmail = true;
      options.SignIn.RequireConfirmedAccount = true;
    });
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
      app.UseExceptionHandler("/Home/Error");
      // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
      app.UseHsts();
    }

    SeedDatabase(app);

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseAuthentication();
    app.UseRouting();

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
