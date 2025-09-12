using DddSample.Application;
using DddSample.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace DddSample.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // App + Infra
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GymLog API v1");
                    c.RoutePrefix = string.Empty;
                }
                );
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/error");

                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        var ex = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;

                        if (ex is ValidationException vex)
                        {
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            context.Response.ContentType = "application/json";

                            var errors = vex.Errors
                                .GroupBy(e => e.PropertyName)
                                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                            await context.Response.WriteAsJsonAsync(new { title = "Validation failed", status = 400, errors });
                            return;
                        }

                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsJsonAsync(new { title = "Unexpected error", status = 500 });
                    });
                });
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
