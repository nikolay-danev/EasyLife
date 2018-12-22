using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasyLife.Persistence.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EasyLife.Domain.Models;
using EasyLife.Application.Services.Interfaces;
using EasyLife.Application.Services;
using  AutoMapper;
using EasyLife.Web.Client.Hubs;
using EasyLife.Web.Client.Profile;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace EasyLife.Web.Client
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<EasyLifeDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
			services.AddDefaultIdentity<User>()
				.AddRoles<IdentityRole>()
				.AddRoleManager<RoleManager<IdentityRole>>()
				.AddEntityFrameworkStores<EasyLifeDbContext>();

			services.AddResponseCompression();
			services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            });

			services.AddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory<User, IdentityRole>>();

            services.AddAuthentication()
                .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["web:client_id"];
                googleOptions.ClientSecret = Configuration["web:client_secret"];
            })
            .AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["facebook:appId"];
                facebookOptions.AppSecret = Configuration["facebook:appSecret"];
            }); ;

			services.AddScoped<IServiceManager, ServiceManager>();
			services.AddScoped<IAdvertisementManager, AdvertisementManager>();
	        services.AddAutoMapper(x => x.AddProfile(new EasyLifeProfile()));
	        services.AddScoped<IOfficeManager, OfficeManager>();
	        services.AddScoped<IOrderManager, OrderManager>();
	        services.AddScoped<IMessageManager, MessageManager>();
	        services.AddScoped<IEmployeeManager, EmployeeManager>();
	        services.AddScoped<IEmailSender, EmailSender>();

	        services.AddSignalR();
	        services.AddResponseCaching();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
			app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseCookiePolicy();
	        app.UseResponseCaching();

			app.UseAuthentication();
	        app.UseSignalR(routes => { routes.MapHub<MessageHub>("/message"); });
			app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
