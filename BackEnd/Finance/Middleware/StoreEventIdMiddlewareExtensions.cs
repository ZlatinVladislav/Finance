using Finance.Middleware.Implementation;
using Microsoft.AspNetCore.Builder;

namespace Finance.Middleware
{
    public static class StoreEventIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }

        public static IApplicationBuilder UseErrorLoggingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorLoggingMiddleware>();
        }
    }
}