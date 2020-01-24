using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using weblab2.Data;

namespace weblab2
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection"),
                    mysqlOptions =>
                    {
                        mysqlOptions.ServerVersion(new ServerVersion(new Version(5, 7, 23), ServerType.MySql))
                            .CharSetBehavior(CharSetBehavior.AppendToAllAnsiColumns)
                            .CharSet(CharSet.Latin1)
                            .CharSetBehavior(CharSetBehavior.AppendToAllColumns)
                            .CharSet(CharSet.Utf8Mb4)
                            .MigrationsAssembly("weblab2")
                            .MigrationsHistoryTable("EF_Migrations");
                    }
                )
            );

            services.AddLocalization(options => { options.ResourcesPath = "Resources"; });

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0)
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
                .AddMvcOptions(options => { options.EnableEndpointRouting = false; });

            services.AddControllersWithViews();
            //services.AddRazorPages();
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

            var supportedCultures = new List<CultureInfo>
                    {
                        //new CultureInfo("en"),
                        new CultureInfo("en-GB"),
                        new CultureInfo("en-US"),
                        //new CultureInfo("fr"),
                        new CultureInfo("fr-FR"),
                        new CultureInfo("fr-CH")
                    };

            /*            string ccTLD = "fr";
                        /*string gTLD = "com|fr|pizza";
                        string grTLD = "|biz|name|pro";
                        string rTLD = "localhost";
                        string ggrrTLD = "";* /
                        RequestCulture ccTLDRequestCulture = ccTLD.Equals(null) ? new RequestCulture("en") : new RequestCulture(ccTLD);*/

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                // RequestCulture global for request network origin
                //                DefaultRequestCulture = ccTLDRequestCulture,
                DefaultRequestCulture = new RequestCulture("en-GB"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            /* REMOVED FROM DEFAULT MVC PROJECT
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });*/
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
