using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NewsSite.DataAccess;
using SQLitePCL;

namespace NewsSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<MessageDbContext>();

            var app = builder.Build();
            
            var scope = app.Services.CreateScope();
            var dataBase = scope.ServiceProvider.GetService<MessageDbContext>();
            dataBase.Database.EnsureCreated();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Message/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Message}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
