using bugLog.Models;
using bugLog.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BikeRentalApi.Models.Repositories;
using Microsoft.AspNetCore.Identity;

namespace bugLog
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
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddControllersWithViews();
            services.AddDbContext<BugDbContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:bugLogConnection"]));
            services.AddDbContext<IdentityContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:IdentityConnection"]));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
            //services.Configure<IdentityOptions>(opts =>
            //{
            //    opts.Password.RequiredLength = 8;
            //    opts.Password.RequireNonAlphanumeric = true;
            //    opts.Password.RequireUppercase = true;
            //    opts.Password.RequireLowercase = true;
            //    opts.Password.RequireDigit = true;
            //    opts.User.RequireUniqueEmail = true;
            //    opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz@";
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BugDbContext context)
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            SeedData.SeedDatabase(context);
        }
    }
}
