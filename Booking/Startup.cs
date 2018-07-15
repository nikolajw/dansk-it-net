using System;
using System.IO;
using System.Reflection;
using Booking.Booking;
using CorrelationId;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Booking
{
    public class Startup
    {
        private string appName = "Hôtel - Booking Service API";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCorrelationId();
            services.AddMediatR();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = appName, Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            
            services.AddSingleton<IBookingRepository, BookingRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", appName);
            });

            app.UseCorrelationId(new CorrelationIdOptions { 
                Header = "X-Correlation-ID", 
                IncludeInResponse = true
            });
            
            app.UseMvc();
        }
    }
}
