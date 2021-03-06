using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using dynamify.Models;
using Microsoft.EntityFrameworkCore;
using dynamify.ServerClasses.QueryClasses;
using dynamify.Configuration;
using dynamify.ServerClasses.Email;
using Microsoft.AspNetCore.Http;

namespace dynamify
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _env { get; }
        
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            ConfSettings.Configuration = Configuration;
            _env = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
                    {
                        options.InputFormatters.Insert(0, new RawJsonBodyInputFormatter());
                    }
                );

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
             
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            string psqlConnection;
            if(_env.IsDevelopment()){
                psqlConnection = Configuration["LocalConnectionString"];
            }else{
                psqlConnection = Configuration["ProductionConnectionString"];
            }

            System.Console.WriteLine(psqlConnection);
            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(psqlConnection));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<AdminQueries>();
            services.AddScoped<SiteQueries>();
            services.AddScoped<AnalyticsQueries>();
            services.AddScoped<Mailer>();

            services.AddSwaggerDocument();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

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

            // Register the Swagger generator and the Swagger UI middlewares
            //app.UseOpenApi();
            //app.UseSwaggerUi3();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
