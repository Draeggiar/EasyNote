using EasyNote.Core;
using EasyNote.Core.Files;
using EasyNote.Core.Files.Interfaces;
using EasyNote.Core.Model.DbEntities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using AutoMapper;

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
            services.AddScoped<IDbContext, FilesDbContext>();

            services.AddAutoMapper();

            services.AddScoped<IFilesManager, FilesManager>();

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
                Name = "Plik1.txt",
                Author = "a.nowak",
                Content = "coœ"
            },
                new FileEntity
                {
                    Name = "Plik2.txt",
                    Author = "b.kowalski",
                    Content = string.Empty
                });

            dbContext.SaveChanges();
        }
    }
}
