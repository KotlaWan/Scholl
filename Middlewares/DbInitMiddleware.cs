using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using laba2.Models;

namespace laba2.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class DbInitMiddleware
    {
        private readonly RequestDelegate _next;

        public DbInitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, IServiceProvider serviceProvider, ApplicationContext dbContext)
        {
            if (!(httpContext.Session.Keys.Contains("starting")))
            {
                DbInitializer.Initialize(dbContext);
                httpContext.Session.SetString("starting", "Yes");
            }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class DbInitMiddlewareExtensions
    {
        public static IApplicationBuilder UseDbInitMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DbInitMiddleware>();
        }
    }
}
