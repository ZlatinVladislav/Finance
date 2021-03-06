using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Finance.Middleware.Implementation
{
    public class ErrorLoggingMiddleware
    {
        private const string MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {RequestCode}";
        private static readonly ILogger Log = Serilog.Log.ForContext<ErrorLoggingMiddleware>();
        private readonly RequestDelegate _next;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                var request = httpContext.Request;

                var log = Log
                    .ForContext("RequestHeaders", request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
                        true)
                    .ForContext("RequestHost", request.Host)
                    .ForContext("RequestProtocol", request.Protocol);

                if (request.HasFormContentType)
                    log = log.ForContext("RequestForm", request.Form.ToDictionary(v => v.Key, v => v.Value.ToString()));

                log.Error(e, MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, 500);

                throw;
            }
        }
    }
}