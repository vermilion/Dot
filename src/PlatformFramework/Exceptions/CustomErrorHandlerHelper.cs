using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlatformFramework.Exceptions
{
    public static class CustomErrorHandlerHelper
    {
        public static void UseCustomErrors(this IApplicationBuilder app, IHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.Use(WriteDevelopmentResponse);
            }
            else
            {
                app.Use(WriteProductionResponse);
            }
        }

        private static Task WriteDevelopmentResponse(HttpContext httpContext, Func<Task> next)
            => WriteResponse(httpContext, true);

        private static Task WriteProductionResponse(HttpContext httpContext, Func<Task> next)
            => WriteResponse(httpContext, false);

        private static async Task WriteResponse(HttpContext httpContext, bool includeDetails)
        {
            // Try and retrieve the error from the ExceptionHandler middleware
            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
            var ex = exceptionDetails?.Error;

            // Should always exist, but best to be safe!
            if (ex != null)
            {
                // ProblemDetails has it's own content type
                httpContext.Response.ContentType = "application/problem+json";

                ProblemDetails problem;
                if (ex is FrameworkException frameworkException)
                {
                    problem = new ProblemDetails
                    {
                        Status = 500,
                        Title = "An error occured",
                        Detail = ex.Message
                    };

                    var props = frameworkException.Properties();

                    foreach ((string key, object value) in props)
                    {
                        problem.Extensions[key] = value;
                    }
                }
                else
                {
                    var title = includeDetails ? "An error occured: " + ex.Message : "An error occured";
                    var details = includeDetails ? ex.ToString() : null;

                    problem = new ProblemDetails
                    {
                        Status = 500,
                        Title = title,
                        Detail = details
                    };
                }

                // This is often very handy information for tracing the specific request
                var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
                if (traceId != null)
                {
                    problem.Extensions["traceId"] = traceId;
                }

                //Serialize the problem details object to the Response as JSON (using System.Text.Json)
                await JsonSerializer.SerializeAsync(httpContext.Response.Body, problem);
            }
        }
    }
}