using Credo.Domain.Errors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Credo.API.Extensions
{
    public static class GlobalErrorHandlerExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Log.Error($"Problem occurred in {contextFeature.Error}");

                        await context.Response.WriteAsync(new ApiException
                        {
                            StatusCode = context.Response.StatusCode ,
                            Message = "Internal Server Error. Please Try again later"
                        }.ToString());
                    }
                });
            });
        }
    }
}