using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WidgetGallery.Models.Northwind;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace WidgetGallery {
    public class Startup {
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            // Add framework services.
            services.AddMvc();

            services
               .AddLogging()
               .AddDbContext<NorthwindContext>(ConfigureNorthwindContext);

#if DB_LOCALDB
            services.AddEntityFrameworkSqlServer();
#endif

#if DB_SQLITE
            services.AddEntityFrameworkSqlite();
#endif
        }

        static void ConfigureNorthwindContext(IServiceProvider serviceProvider, DbContextOptionsBuilder options) {
            var hosting = serviceProvider.GetRequiredService<IHostingEnvironment>();

#if DB_LOCALDB
            var dbPath = Path.Combine(hosting.ContentRootPath, "Northwind.mdf");
            var connectionString = $@"Data Source=(localdb)\devextreme; AttachDbFileName={dbPath}; Integrated Security=True; MultipleActiveResultSets=True; App=EntityFramework";
            options.UseSqlServer(connectionString);
#endif

#if DB_SQLITE
            var dbPath = Path.Combine(hosting.ContentRootPath, "Northwind.sqlite");
            options.UseSqlite("Data Source=" + dbPath);
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            } else {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
