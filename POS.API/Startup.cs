﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Core;
using POS.Core.Service;

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
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IDiscountVariablesRepository, DiscountVariablesRepository>();
            services.AddScoped<IShoppingCartCalculator, ShoppingCartCalculator>();
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
