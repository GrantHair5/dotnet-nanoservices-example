using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nanoservices.Example.Api.Data;
using Nanoservices.Example.Api.Services;
using Nanoservices.Example.Api.Services.Implementation;
using Newtonsoft.Json;

namespace Nanoservices.Example.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRetrievalService, RetrievalService>();
            services.AddScoped<IDataWriteService, DataWriteService>();
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

                    await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
                });

                endpoints.MapPost("/country", async context =>
                {
                    var writeService = context.Request.HttpContext.RequestServices.GetRequiredService<IDataWriteService>();
                    using var reader = new StreamReader(context.Request.Body);
                    try
                    {
                        var body = await reader.ReadToEndAsync();

                        var objectToAdd = JsonConvert.DeserializeObject<Country>(body);
                        writeService.Add<Country>(objectToAdd);

                        context.Response.StatusCode = 200;
                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = 500;
                    }
                });
            });
        }
    }
}