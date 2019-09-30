using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CarApp.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using CarApp.Repository;
using Newtonsoft.Json;
using AutoMapper;
using CarApp.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace confusionresturant
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
            _config = builder.Build();
        }
        IConfigurationRoot _config;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);
            services.AddDbContext<CarUserContext>(ServiceLifetime.Scoped);
            services.AddScoped<ICarUserRepository, CarUserRepository>();
            services.AddTransient<CarUserInitializer>();
            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<CarUserContext>();
            // Add framework services.
            services.AddMvc()
                   .AddJsonOptions(opt =>
                   {
                       // ignore circular reference 
                       opt.SerializerSettings.ReferenceLoopHandling =
                       ReferenceLoopHandling.Ignore;
                   });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                             IHostingEnvironment env,
                             ILoggerFactory loggerFactory,
                             CarUserInitializer seeder)
        {
            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseIdentity();
            app.UseMvc();
            seeder.Seed().Wait();
        }
    }
}
