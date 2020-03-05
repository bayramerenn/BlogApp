using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace BlogApp.WebUI
{
    public class Startup
    {

        private readonly IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        
       
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<BlogContext>(options => options.UseSqlServer(_config.GetConnectionString("DefaultConnection"),b => b.MigrationsAssembly("BlogApp.WebUI")));
            services.AddControllersWithViews();
            services.AddTransient<IBlogReporsitory, EfBlogRepository>();
            services.AddTransient<ICategoryRepository, EfCategoryRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseStaticFiles();//wwwroot

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "node_modules")
                ),
                RequestPath = "/modules"
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            app.UseStatusCodePages();
            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{Controller=Home}/{action=Index}/{id?}");
            });

            SeedData.Seed(app);
        }
    }
}
