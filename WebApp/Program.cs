var builder = WebApplication.CreateBuilder(args);
//ConfigureServices
builder.Services.AddTransient<object>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    //Configure
    var @object = app.Services.GetService<object>();
    Console.WriteLine(app.Services.GetService<IConfiguration>()["MyData"]);

}


app.Use(async (context, next) =>
{
    Console.WriteLine($"Begin use1 {context.Request.Path}");
    if (context.Request.Path.ToString().Contains(".ico"))
        context.Response.StatusCode = StatusCodes.Status404NotFound;
    else
    {
        await next();
    }
    Console.WriteLine($"End use1 {context.Request.Path}");
});

app.Map("/hello", helloApp =>
{
    helloApp.Use(async (context, next) =>
    {
        Console.WriteLine($"Begin mapUse1 {context.Request.Path}");


        await next();
        Console.WriteLine($"End mapUse1 {context.Request.Path}");
    });


    helloApp.Run(async context =>
    {
        Console.WriteLine($"Begin mapRun {context.Request.Path}");
        await context.Response.WriteAsync("Hello anonymouse!");
        Console.WriteLine($"End mapRun {context.Request.Path}");
    });
});


app.Use(async (context, next) =>
{
    Console.WriteLine($"Begin use2 {context.Request.Path}");


    await next();
    Console.WriteLine($"End use2 {context.Request.Path}");
});


app.MapWhen(context => context.Request.Query.TryGetValue("name", out _), mapWhen =>
{
    mapWhen.Run(async context =>
    {
        Console.WriteLine($"Begin mapWhenRun {context.Request.Path}");
        await context.Response.WriteAsync($"Hello {context.Request.Query["name"]}!");
        Console.WriteLine($"End mapWhenRun {context.Request.Path}");
    });
});


app.Run(async context =>
{
    Console.WriteLine($"Begin run {context.Request.Path}");
    await context.Response.WriteAsync("Hello World!");
    Console.WriteLine($"End run {context.Request.Path}");
});




app.Run();
