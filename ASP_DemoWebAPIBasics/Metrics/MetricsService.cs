using System.Text;

namespace ASP_DemoWebAPIBasics.Metrics
{
    // Very small in-memory metric collector for demo purposes
    public class MetricsService
    {
        private long _requests;

        public void IncrementRequests() => System.Threading.Interlocked.Increment(ref _requests);

        public string GetPrometheusMetrics()
        {
            var sb = new StringBuilder();
            sb.AppendLine("# HELP asp_demowebapibasics_requests_total Total HTTP requests handled by the app");
            sb.AppendLine("# TYPE asp_demowebapibasics_requests_total counter");
            sb.AppendLine($"asp_demowebapibasics_requests_total {_requests}");
            return sb.ToString();
        }
    }
}
