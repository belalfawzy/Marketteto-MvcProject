using Marketteto.Data.Services;
using Marketteto.Models;
using Microsoft.EntityFrameworkCore;
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
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
