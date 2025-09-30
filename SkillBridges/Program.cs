
using Microsoft.EntityFrameworkCore;
using SkillBridges.Data;
using SkillBridges.Mappings;
using SkillBridges.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Authentication.Cookies;

using SkillBridges.Services;

namespace SkillBridges
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            StaticWebAssetsLoader.UseStaticWebAssets(
             builder.Environment,
             builder.Configuration);
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<SkillBridgeContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SkillBridgeConn")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWorkRepository>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login";
                    options.AccessDeniedPath = "/Home/Login";
                });

            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.AddScoped<EmailService>();


           
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

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
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
