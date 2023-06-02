namespace WebApp.Middleware
{
    public class MapRunMiddleware : IMiddleware
    {

        private readonly ILogger _logger;

        public MapRunMiddleware(ILogger<Use1Middleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate _)
        {
            //Console.WriteLine($"Begin mapRun {context.Request.Path}");
            _logger.LogInformation($"Begin mapRun {context.Request.Path}");
            await context.Response.WriteAsync("Hello anonymouse!");
            //Console.WriteLine($"End mapRun {context.Request.Path}");
            _logger.LogInformation($"End mapRun {context.Request.Path}");
        }
    }
}
