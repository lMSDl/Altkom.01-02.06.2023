namespace WebApp.Middleware
{
    public class Use1Middleware : IMiddleware
    {

        private readonly ILogger _logger;

        public Use1Middleware(ILogger<Use1Middleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //Console.WriteLine($"Begin use1 {context.Request.Path}");
            _logger.LogInformation($"Begin use1 {context.Request.Path}");
            if (context.Request.Path.ToString().Contains(".ico"))
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            else
            {
                await next(context);
            }
            //Console.WriteLine($"End use1 {context.Request.Path}");
            _logger.LogInformation($"End use1 {context.Request.Path}");
        }
    }
}
