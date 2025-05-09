using Marketteto.Data.Card;
using Marketteto.Data.Seed;
using Marketteto.Data.Services;
using Marketteto.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Marketteto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<MarkettetoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Data Source=.;Initial Catalog=MarkettetoDB;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True")));
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<ShoppingCard>(x => ShoppingCard.GetCard(x));
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<MarkettetoDbContext>();
            builder.Services.AddMemoryCache();
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();

            AppDbInitializer.SeedUserAndRolesAsync(app).Wait();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
