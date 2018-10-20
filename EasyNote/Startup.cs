using System;
using EasyNote.Model.DbEntities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyNote
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
            services.AddDbContext<FilesDbContext>(o => o.UseInMemoryDatabase("EasyNoteDb"));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                var context = serviceProvider.GetRequiredService<FilesDbContext>();
                SeedTestData(context);

                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }

        private void SeedTestData(FilesDbContext dbContext)
        {
            dbContext.Files.AddRange(new FileEntity
            {
                Id = 1,
                Name = "Plik1.txt",
                Author = "a.nowak",
                Content = new byte[0]
            },
                new FileEntity
                {
                    Id = 2,
                    Name = "Plik2.txt",
                    Author = "b.kowalski",
                    Content = new byte[0]
                });

            dbContext.SaveChanges();
        }
    }
}
