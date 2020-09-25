using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nanoservices.Example.Api.Data;
using Nanoservices.Example.Api.Services;
using Nanoservices.Example.Api.Services.Implementation;

namespace Nanoservices.Example.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRetrievalService, RetrievalService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/countries/{id:int}", async context =>
                {
                    var id = int.Parse((string)context.Request.RouteValues["id"]);
                    var retrievalService = context.Request.HttpContext.RequestServices.GetRequiredService<IRetrievalService>();
                    var response = retrievalService.Retrieve(id);
                    await context.Response.WriteAsync(response);
                });

                endpoints.MapGet("/allcountries", async context =>
                {
                    var retrievalService = context.Request.HttpContext.RequestServices.GetRequiredService<IRetrievalService>();
                    var response = retrievalService.RetrieveAll();

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
