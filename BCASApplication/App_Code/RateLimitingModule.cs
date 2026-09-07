// [SECURITY AUDIT May-2026] NEW FILE — IP-based rate limiter for the login endpoint.
// Audit finding: No Rate Limiting (High). Limits login POSTs to 10 per IP per 5 minutes.
// Registered in web.config <httpModules> (IIS 6) and <system.webServer><modules> (IIS 10).
// [END SECURITY AUDIT]
using System;
using System.Collections.Concurrent;
using System.Web;

namespace BCASApplication
{
    public class RateLimitingModule : IHttpModule
    {
        // Max login attempts per IP within the window
        private const int MaxAttempts = 10;
        private static readonly TimeSpan Window = TimeSpan.FromMinutes(5);

        // thread-safe store: IP -> (attempt count, window start)
        private static readonly ConcurrentDictionary<string, Tuple<int, DateTime>> _store
            = new ConcurrentDictionary<string, Tuple<int, DateTime>>(StringComparer.OrdinalIgnoreCase);

        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
        }

        private void OnBeginRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            var request = app.Request;

            // Only rate-limit the login page
            if (!request.AppRelativeCurrentExecutionFilePath.Equals("~/Home.aspx", StringComparison.OrdinalIgnoreCase))
                return;

            // Only POST (actual login submissions)
            if (!string.Equals(request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase))
                return;

            string ip = GetClientIp(request);
            if (string.IsNullOrEmpty(ip))
                return;

            var now = DateTime.UtcNow;

            _store.AddOrUpdate(
                ip,
                _ => Tuple.Create(1, now),
                (_, existing) =>
                {
                    if (now - existing.Item2 > Window)
                        return Tuple.Create(1, now);   // window expired, reset
                    return Tuple.Create(existing.Item1 + 1, existing.Item2);
                });

            var entry = _store[ip];
            if (entry.Item1 > MaxAttempts)
            {
                var response = app.Response;
                response.StatusCode = 429;
                response.StatusDescription = "Too Many Requests";
                response.ContentType = "text/plain";
                response.Write("Too many login attempts. Please try again after 5 minutes.");
                response.End();
            }
        }

        private static string GetClientIp(HttpRequest request)
        {
            // Respect reverse-proxy / load-balancer forwarded IP
            string ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ip))
            {
                // X-Forwarded-For may be a comma-separated list; take the first (original client)
                int comma = ip.IndexOf(',');
                ip = comma >= 0 ? ip.Substring(0, comma).Trim() : ip.Trim();
            }
            if (string.IsNullOrEmpty(ip))
                ip = request.UserHostAddress;
            return ip;
        }

        public void Dispose() { }
    }
}
