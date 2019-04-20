using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace POS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /* 
               ASP.NET Core implements dependency injection by default.Services(such as the EF database context) are registered
               with dependency injection during application startup. Components that require these services(such as MVC controllers) 
               are provided these services via constructor parameters.
        */
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IDiscount, Discount>();
            //services.AddScoped<ICalculateTotal, CalculateTotal>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
