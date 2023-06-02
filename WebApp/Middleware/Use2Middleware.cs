namespace WebApp.Middleware
{
    public class Use2Middleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public Use2Middleware(RequestDelegate next, ILogger<Use2Middleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) {
            //Console.WriteLine($"Begin use2 {context.Request.Path}");
            _logger.LogInformation($"Begin use2 {context.Request.Path}");

            await _next(context);
            //Console.WriteLine($"End use2 {context.Request.Path}");
            _logger.LogInformation($"End use2 {context.Request.Path}");
        }
    }
}
