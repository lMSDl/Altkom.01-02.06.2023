namespace WebApp.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        public static void Use1Middleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<Use1Middleware>();
        }
        public static void Use2Middleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<Use2Middleware>();
        }

        public static void MapRunMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<MapRunMiddleware>();
        }
    }
}
