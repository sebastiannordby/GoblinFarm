using App.Classes;
using App.Services;
using App.Services.Hosted;
using GoblinFarm.EF;
using GoblinFarm.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GoblinFarm
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
            Configuration.Bind("TelldusSettings", SystemVariables.TelldusSettings);
            Configuration.Bind("HiveOSSettings", SystemVariables.HiveOSSettings);
            Configuration.Bind("CoinbaseSettings", SystemVariables.CoinbaseSettings);
            Configuration.Bind("SupportedCoins", SystemVariables.SupportedCoins);

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });


            services.AddDbContext<GoblinFarmContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));


            });

            services.AddScoped<ITelldusSensorService, TelldusSensorService>();
            services.AddScoped<IHiveOSService, HiveOSService>();
            services.AddScoped<IHiveOnService, HiveOnService>();
            services.AddScoped<ICoinbaseService, CoinbaseService>();
            services.AddScoped<IFjordkraftService, FjordkraftService>();

            services.AddHostedService<ElectricityLoggingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var appDbContext = serviceScope.ServiceProvider.GetRequiredService<GoblinFarmContext>();

                try
                {
                    appDbContext.Database.Migrate();
                }
                catch (Exception exception)
                {
                    var hell = exception;
                }
            }
        }
    }
}
