using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace src
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
             services.AddSingleton(_config);  
         services.AddDbContext<CarUserContext>(ServiceLifetime.Scoped);  
         services.AddScoped<ICarUserRepository, CarUserRepository>();  
         services.AddTransient<CarUserInitializer>();  
         services.AddIdentity<User, IdentityRole>()  
                 .AddEntityFrameworkStores<CarUserContext>();  
         services.AddAutoMapper(); 
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              ILoggerFactory loggerFactory,
                              CarUserInitializer seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            loggerFactory.AddConsole(_config.GetSection("Logging"));  
            loggerFactory.AddDebug();  
            app.UseIdentity();  
            seeder.Seed().Wait(); 
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
