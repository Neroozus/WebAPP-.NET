using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TestAuth.Interfaces;
using TestAuth.Mapping;
using TestAuth.Models;
using TestAuth.Services;
using TestAuth.Services.Repositories;

namespace TestAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IComputerPartsRepo, ComputerPartsRepo>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;   // ����������� �����
                options.Password.RequireNonAlphanumeric = false;   // ��������� �� �� ���������-�������� �������
                options.Password.RequireLowercase = false; // ��������� �� ������� � ������ ��������
                options.Password.RequireUppercase = false; // ��������� �� ������� � ������� ��������
                options.Password.RequireDigit = false; // ��������� �� �����
                options.User.RequireUniqueEmail = true;    // ���������� email
              //  options.User.AllowedUserNameCharacters = ".@abcdefghijklmnopqrstuvwxyz0123456789"; // ���������� �������
            })
                .AddEntityFrameworkStores<UserContext>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddDefaultTokenProviders();

            // ��������� ������������ �����������
            // services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //   .AddCookie(options => //CookieAuthenticationOptions
            // {
            //   options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
            //});
            services.AddLocalization(options => options.ResourcesPath = "Resources");
           // services.AddSession();
            services.AddControllersWithViews()           
             .AddDataAnnotationsLocalization() // ��������� ����������� ���������;
                .AddViewLocalization();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {

                app.UseExceptionHandler("/error");

            }

            var supportedCultures = new[]
           {

                new CultureInfo("ru"),

            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ru"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();    // ��������������
            app.UseAuthorization();     // �����������
            //app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
