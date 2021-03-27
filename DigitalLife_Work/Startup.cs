using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalLife_Work.Models.BLL;
using DigitalLife_Work.Models.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DigitalLife_Work
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(_configuration["ConnectionStrings:Default"]);
            });

            services.AddIdentity<AppUser, IdentityRole>(options => options.Stores.MaxLengthForKeys = 128)

                    .AddEntityFrameworkStores<MyContext>();
            services.AddMvc();
        }

        //Add roles to db
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            string firstRoleName = "Admin";
            string secondRoleName = "User";

            IdentityResult roleResult;

            var roleExist = await RoleManager.RoleExistsAsync(firstRoleName);
            var roleExist2 = await RoleManager.RoleExistsAsync(secondRoleName);

            if (!roleExist)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole(firstRoleName));
            }

            if (!roleExist2)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole(secondRoleName));
            }
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{url?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                 name: "areas", "WebCms",
                 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });

            Task.Run(() => CreateRoles(serviceProvider)).Wait();

        }
    }
}
