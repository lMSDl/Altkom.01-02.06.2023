namespace WebApp.Middleware
{
    public class MapWhenRunMiddleware
    {
        private readonly ILogger _logger;

        public MapWhenRunMiddleware(RequestDelegate _, ILogger<Use2Middleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) {
            //Console.WriteLine($"Begin mapWhenRun {context.Request.Path}");
            _logger.LogInformation($"End mapWhenRun {context.Request.Path}");
            await context.Response.WriteAsync($"Hello {context.Request.Query["name"]}!");
            //Console.WriteLine($"End mapWhenRun {context.Request.Path}");
            _logger.LogInformation($"End mapWhenRun {context.Request.Path}");
        }
    }
}
