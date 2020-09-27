using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager_MVC.Models;
using EmployeeManager_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EmployeeManager_MVC.Security;

namespace EmployeeManager_MVC
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

            // AddDbContext() method registers AppDbContext with the DI container
            // Connection string passed to UseSqlServer
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("AppDb")));

            // In this example, we are using the same db for the for identity as our above.
            // Often they will be in different DB
            services.AddDbContext<AppIdentityDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("AppDb")));

            // AddIdentity() registers ASP.NET core Identity services with DI Container
            // AddEntityFrameworkStores() adds an EF Core implementation of identity data stores
            services.AddIdentity<AppIdentityUser, AppIdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            // By default ASp.NET Core Identity issues a cookie to an authenticed user.
            // Below we've configured the Cookie for the login page's path.
            // Any unauthorized attempts  (no cookie) redirected to login page.
            // AccessDeniedPath sets display page path for cases of access attempts denied.
            // For example a authenticated user attempting to access resources that user is not authorized to.
            services.ConfigureApplicationCookie(opt =>
           {
               opt.LoginPath = "/Security/SignIn";
               opt.AccessDeniedPath = "/Security/AccessDenied";
           });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
