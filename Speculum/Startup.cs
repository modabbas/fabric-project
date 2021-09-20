using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FabrciProject.IData.Interfaces;
using FabricProject.Data.Repositories;
using FabricProject.DContext;
using FabricProject.Dto.Machine;
using FabricProject.Models.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Speculum
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<FabricProjectDbContext>
             (options =>
             {
                 options.UseSqlServer
                         (Configuration.GetConnectionString("DefaultConnection"));
             });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Account/Login"); ;
                //  options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Home/Error";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.SlidingExpiration = true;
            });

            services.AddAuthentication();
            services.AddAuthorization();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddIdentity<FbUser, FbRole>(identity =>
            {
                identity.Password.RequiredLength = 4;
                identity.Password.RequireNonAlphanumeric = false;
                identity.Password.RequireLowercase = false;
                identity.Password.RequireUppercase = false;
                identity.Password.RequireDigit = false;
            })
              .AddEntityFrameworkStores<FabricProjectDbContext>()
              .AddDefaultTokenProviders();
            ConfigureFactoricProject(services);
        }

        private void ConfigureFactoricProject(IServiceCollection services)
        {


            services.AddScoped<ICustomerOrderRepository, CustomerOrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IClothRespository, ClothRepository>();
            services.AddScoped<ILabRepository, LabRepository>();
            services.AddScoped<IMachineRepository, MachineRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<ICustomerOrderDetailsMachineRepository, CustomerOrderDetailsMachineRepository>();
            services.AddScoped<ICustomerOrderDetailsRepository, CustomerOrderDetailsRepository>();
            services.AddScoped<IDeliverRepository, DeliverRepository>();
            services.AddScoped<IStatisticsRepository, StatisticsRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IClothMaterialRepository, ClothMaterialRepository>();
            services.AddScoped<IOrderMaterialsRepository, OrderMaterialRepository>();
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });

        }
    }
}
