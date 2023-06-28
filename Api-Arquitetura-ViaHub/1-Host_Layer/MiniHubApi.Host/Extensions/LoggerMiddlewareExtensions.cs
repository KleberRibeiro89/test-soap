using MiniHubApi.Application.Interfaces;

namespace MiniHubApi.Host.Extensions
{
    public class LoggerMiddlewareExtensions
    {
        private readonly RequestDelegate _next;
        //private readonly ILogCustomService _loggerCustomService;
        private readonly ILogger _logger;

        public LoggerMiddlewareExtensions(
            RequestDelegate next, 
            ILogCustomServices logger, 
            ILoggerFactory loggerFactory)
        {
            _next = next;
           // _loggerCustomService = logger;
            _logger = loggerFactory.CreateLogger<LoggerMiddlewareExtensions>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                _logger.LogInformation(
                    "Request {method} {url} => {statusCode}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode);
            }
        }
    }
}
