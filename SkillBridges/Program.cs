
using Microsoft.EntityFrameworkCore;
using SkillBridges.Data;
using SkillBridges.Mappings;
using SkillBridges.Models;
using AutoMapper;
namespace SkillBridges
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<SkillBridgeContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SkillBridgeConn")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWorkRepository>();
            builder.Services.AddScoped<IUnitOfWork2, UnitOfWorkRepository2>();


            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Helper());
            });
            var mapper = config.CreateMapper();
            builder.Services.AddSingleton(mapper);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Login}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
