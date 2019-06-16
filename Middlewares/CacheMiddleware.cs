using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using laba2.Services;
using laba2.Models;

namespace laba2.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _memoryCache;
        private string _cacheKey;

        public CacheMiddleware(RequestDelegate next, IMemoryCache memoryCache, string cacheKey = "PriceFuels")
        {
            _next = next;
            _memoryCache = memoryCache;
            _cacheKey = cacheKey;
        }

        public Task Invoke(HttpContext httpContext, DbService service)
        {
            List<PriceClass> list;
            
            if (!_memoryCache.TryGetValue(_cacheKey, out list))
            {
                list = service.GetPriceFuels();
                
                _memoryCache.Set(_cacheKey, list,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(2 * 10 + 240)));
            }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CacheMiddlewareExtensions
    {
        public static IApplicationBuilder UseCacheMiddleware(this IApplicationBuilder builder, string key)
        {
            return builder.UseMiddleware<CacheMiddleware>(key);
        }
    }
}
