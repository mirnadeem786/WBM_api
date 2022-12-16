using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Text.Json.Serialization;
using System.Web.Http;
using WillowBatMarketWebApiService.BusinessLayer;
using WillowBatMarketWebApiService.DataLayer;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "APIPolicy";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
             
        {

            services.AddHttpContextAccessor();
            services.AddDbContext<AppDbContext>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.AddScoped<IBatRepository, BatRepository>();
            services.AddScoped<IWillowSellerRepository, WillowSellerRepository>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
           services.AddScoped<IWillowRepository, WillowRepository>();
            services.AddScoped<ICricketerRepository, CricketerRepository>();
            services.AddScoped<ICricketerDashboardRepository,CricketerDashboardRepository>();
            services.AddScoped<ImanufacturerDashboardRepository, ManufacturerDashboardRepository>();
            services.AddScoped<IwillowSellerDashboardRepo, WillowSellerDashboardRepo>();
            services.AddScoped<IUsserRepository,UsserRepository>();
            services.AddScoped<IimageManupulation, ImageManupulation>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());            // add this line to apply conversion globally and not only for one property

            services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                // ignore omitted parameters on models to enable optional params (e.g. User update)
                x.JsonSerializerOptions.IgnoreNullValues = true;
            });



            //services.AddTransient<IWillowSellerRepository, WillowSellerREpository>();
            // services.AddTransient<IBatRepository,BatRepository>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WillowBatMarketWebApiService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WillowBatMarketWebApiService v1"));
            }

            app.UseRouting();
                app.UseDefaultFiles();
                app.UseStaticFiles();
                app.UseCors(MyAllowSpecificOrigins);


          

            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
    
        }
      
        }
}
