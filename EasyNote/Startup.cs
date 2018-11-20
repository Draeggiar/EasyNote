using AutoMapper;
using EasyNote.Core;
using EasyNote.Core.Logic.Accounts;
using EasyNote.Core.Logic.Files;
using EasyNote.Core.Model.Accounts;
using EasyNote.Core.Model.DbEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.FileProviders;

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
      services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
          b => b.MigrationsAssembly("EasyNote")));
      services.AddScoped<IDbContext, ApplicationDbContext>();

      services.AddAutoMapper();

      AddIdentity(services);
      ConfigureAuthentication(services);

      services.AddScoped<IFilesManager, FilesManager>();
      services.AddScoped<IAccountsManager, AccountsManager>();

      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseAuthentication();
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

      //app.UseSpa(spa =>
      //{
      //  spa.Options.SourcePath = "ClientApp";
      //  if (env.IsDevelopment())
      //  {
      //    spa.UseAngularCliServer(npmScript: "start");
      //  }
      //});
    }

    private static void AddIdentity(IServiceCollection services)
    {
      var builder = services.AddIdentityCore<UserEntity>(o =>
      {
              // configure identity options
              o.Password.RequireDigit = false;
        o.Password.RequireLowercase = false;
        o.Password.RequireUppercase = false;
        o.Password.RequireNonAlphanumeric = false;
        o.Password.RequiredLength = 4;
      });
      builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
      builder.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
    }

    private void ConfigureAuthentication(IServiceCollection services)
    {
      var secretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH";
      var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
      var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

      // Configure JwtIssuerOptions
      services.Configure<JwtIssuerOptions>(options =>
      {
        options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
        options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
        options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
      });

      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

        ValidateAudience = true,
        ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = signingKey,

        RequireExpirationTime = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
      };

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(configureOptions =>
      {
        configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
        configureOptions.TokenValidationParameters = tokenValidationParameters;
        configureOptions.SaveToken = true;
      });

      // api user claim policy
      services.AddAuthorization(options =>
      {
        options.AddPolicy("ApiUser", policy => policy.RequireClaim(JwtFactory.JwtClaimIdentifiers.Rol, JwtFactory.JwtClaims.ApiAccess));
      });

      services.AddSingleton<IJwtFactory, JwtFactory>();
    }
  }
}
