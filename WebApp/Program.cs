using WebApp.Middleware;

var builder = WebApplication.CreateBuilder(args);
//ConfigureServices
builder.Services.AddTransient<object>();

//builder.Services.AddTransient<Use1Middleware>();
builder.Services.Use1Middleware();
builder.Services.AddTransient<MapRunMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //Configure
    var @object = app.Services.GetService<object>();
    Console.WriteLine(app.Services.GetService<IConfiguration>()["MyData"]);
}

//app.UseMiddleware<Use1Middleware>();
app.Use1Middleware();
app.Map("/hello", MapHello());
app.UseMiddleware<Use2Middleware>();
app.MapWhen(context => context.Request.Query.TryGetValue("name", out _), MapWhenName());
app.Run(async context =>
{
    Console.WriteLine($"Begin run {context.Request.Path}");
    await context.Response.WriteAsync("Hello World!");
    Console.WriteLine($"End run {context.Request.Path}");
});

app.Run();


static Action<IApplicationBuilder> MapWhenName()
{
    return mapWhenApp =>
    {
        mapWhenApp.UseMiddleware<MapWhenRunMiddleware>();
    };
}


static Action<IApplicationBuilder> MapHello()
{
    return helloApp =>
    {
        helloApp.Use(async (context, next) =>
        {
            Console.WriteLine($"Begin mapUse1 {context.Request.Path}");
            await next();
            Console.WriteLine($"End mapUse1 {context.Request.Path}");
        });


        //helloApp.UseMiddleware<MapRunMiddleware>();
        helloApp.MapRunMiddleware();
    };
}