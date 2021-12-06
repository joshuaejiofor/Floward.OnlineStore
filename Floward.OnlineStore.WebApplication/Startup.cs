using Floward.OnlineStore.ApplicationCore.DependencyInjection;
using Floward.OnlineStore.ApplicationCore.Factories;
using Floward.OnlineStore.ApplicationCore.Factories.Interfaces;
using Floward.OnlineStore.ApplicationCore.Services;
using Floward.OnlineStore.ApplicationCore.Services.Interfaces;
using Floward.OnlineStore.Core.UnitOfWork;
using Floward.OnlineStore.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Linq;

namespace Floward.OnlineStore.WebApplication
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
            services.AddScoped<FlowardOnlineStoreDbContext>()
                    .AddScoped<IUnitOfWork, UnitOfWork>()
                    .AddScoped<IProductService, ProductService>()
                    .AddScoped<IProductProviderFactory, ProductProviderFactory>()
                    .AddProviders()
                    .AddSingleton(c => Log.Logger);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FlowardOnlineStoreDbContext flowardOnlineStoreDbContext)
        {
            flowardOnlineStoreDbContext.Database.EnsureCreated();
            SeedDB(flowardOnlineStoreDbContext);
            
            app.UseHttpsRedirection()
               .UseStaticFiles()
               .UseRouting()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute( name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                });

#if DEBUG
            app.UseDeveloperExceptionPage();
#else
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
#endif
        }

        protected void SeedDB(FlowardOnlineStoreDbContext dbContext)
        {
            bool changes = false;

            if (!dbContext.Roles.Any())
            {
                changes = true;
                string roleGuid = Guid.NewGuid().ToString();
                string name = "admin";
                string email = "admin@floward.com";
                dbContext.Roles.Add(new IdentityRole()
                {
                    Id = roleGuid,
                    Name = name,
                    NormalizedName = name.ToUpper(),
                    ConcurrencyStamp = roleGuid
                });

                if (!dbContext.Users.Any())
                {
                    var hasher = new PasswordHasher<IdentityUser>();
                    string userGuid = Guid.NewGuid().ToString();
                    var adminUser = new IdentityUser()
                    {
                        Id = userGuid,
                        UserName = email,
                        NormalizedUserName = email.ToUpper(),
                        Email = email,
                        NormalizedEmail = email.ToUpper(),
                        EmailConfirmed = true,
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    };
                    adminUser.PasswordHash = hasher.HashPassword(adminUser, "Password12");
                    dbContext.Users.Add(adminUser);
                    dbContext.UserRoles.Add(new IdentityUserRole<string>()
                    {
                        UserId = userGuid,
                        RoleId = roleGuid
                    });
                }
            }

            if (changes) dbContext.SaveChanges();
        }
    }
}
