using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ASP_DemoWebAPIBasics.Metrics
{
    public class TracingAndMetricsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MetricsService _metrics;

        public TracingAndMetricsMiddleware(RequestDelegate next, MetricsService metrics)
        {
            _next = next;
            _metrics = metrics;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var activity = new Activity("http_request");
            activity.Start();
            activity.AddTag("http.method", context.Request.Method);
            activity.AddTag("http.path", context.Request.Path);

            _metrics.IncrementRequests();

            try
            {
                await _next(context);
                activity.AddTag("http.status_code", context.Response.StatusCode.ToString());
            }
            finally
            {
                activity.Stop();
            }
        }
    }
}
