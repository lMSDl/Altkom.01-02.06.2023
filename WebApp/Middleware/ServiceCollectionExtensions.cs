namespace WebApp.Middleware
{
    public static class ServiceCollectionExtensions
    {
        public static void Use1Middleware(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<Use1Middleware>();
        } 
    }
}
