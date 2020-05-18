using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using DungeonRpg.Engine;
using DungeonRpg.Services;

namespace DungeonRpg
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddHttpContextAccessor();
            services.AddScoped<HttpContextAccessor>();
            services.AddSingleton<ComponentService>();
            services.AddSingleton<RaceService>();
            services.AddSingleton<ItemService>();
            services.AddSingleton<PlayerService>();
            services.AddSingleton<NpcService>();
            services.AddSingleton<EnemyService>();
            services.AddSingleton<MapService>();
            services.AddScoped<ActionProvider>();
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
                app.UseExceptionHandler("/Error");
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
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            InitializeData(app);
        }

        public void InitializeData(IApplicationBuilder app)
        {
            app.ApplicationServices.GetService<PlayerService>().InitializeInitialData();
            app.ApplicationServices.GetService<RaceService>().InitializeInitialData();
            app.ApplicationServices.GetService<ItemService>().InitializeInitialData();
            app.ApplicationServices.GetService<EnemyService>().InitializeInitialData();
            app.ApplicationServices.GetService<NpcService>().InitializeInitialData();
            app.ApplicationServices.GetService<MapService>().InitializeInitialData();
        }
    }
}
