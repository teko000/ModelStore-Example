﻿using SnapObjects.Data;
using SnapObjects.Data.AspNetCore;
using PowerBuilder.Data.AspNetCore;
using SnapObjects.Data.Oracle;
using Appeon.ModelStoreDemo.Oracle.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;

namespace Appeon.ModelStoreDemo.Oracle
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(m =>
            {
                m.UseCoreIntegrated();
				m.UsePowerBuilderIntegrated();
            });

           // Note: Replace "ContextName" with the configuration context name; replace "key" with the database connection name that exists in appsettings.json. The sample code is as follows:
            services.AddDataContext<OrderContext>(m => m.UseOracle(Configuration["ConnectionStrings:AdventureWorks"]));

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISalesOrderService, SalesOrderService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IOrderReportService, OrderReportService>();

            services.AddGzipCompression(CompressionLevel.Fastest);

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseHsts();
            }
            //app.UseHttpsRedirection();

            app.UseResponseCompression();

            app.UseMvc();
			
        }
    }
}

