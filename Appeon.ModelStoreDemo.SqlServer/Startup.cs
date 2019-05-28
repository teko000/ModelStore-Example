using SnapObjects.Data;
using SnapObjects.Data.AspNetCore;
using PowerBuilder.Data.AspNetCore;
using SnapObjects.Data.SqlServer;
using Appeon.ModelStoreDemo.SqlServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;

namespace Appeon.ModelStoreDemo.SqlServer
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

	    //Note: Change "OrderContext" if you have changed the default DataContext file name; change the "AdventureWorks" if you have changed the database connection name in appsettings.json            
            services.AddDataContext<OrderContext>(m => m.UseSqlServer(Configuration["ConnectionStrings:AdventureWorks"]));

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

