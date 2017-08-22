using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GRS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using GRS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GRS.ApplicationCore.Interfaces;
using GRS.Infrastructure.Logging;
using Microsoft.AspNetCore.Identity;
using GRS.Web.Interfaces;
using GRS.Web.Services;

namespace GRS.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GRSContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("GRSConnection")));
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("GRSIdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped<ICandidateService, CandidateService>();

            // Add session related services.
            services.AddMemoryCache();
            services.AddSession();

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseSession();
            app.UseStaticFiles();
            app.UseIdentity();            

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "Error", template: "Error",
                    defaults: new { controller = "Error", action = "Error" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });

            //Seed Data
            GRSContextSeed.SeedAsync(app, loggerFactory).Wait();
            AppIdentityDbContextSeed.SeedAsync(app).Wait();            
        }
    }
}
