using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();




app.Use(async (context, next) =>
{
    Console.WriteLine(context.GetEndpoint()?.DisplayName ?? "null");
    await next();

});

app.UseRouting();


app.Use(async (context, next) =>
{
    Console.WriteLine(context.GetEndpoint()?.DisplayName ?? "null");
    await next();

    });


app.Map("/demo", mapApp =>
{
    mapApp.UseRouting();

    mapApp.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", () => "Hello demo World!");
    endpoints.MapGet("/{name}", (string name) => $"Hello demo {name}!");
    endpoints.MapGet("/bye", () => "Bye demo World!");
});

    mapApp.Run(async context =>
    {
        await context.Response.WriteAsync("Under construction!");
    });

});


//przy niejawnej delkaracji enpointów, middleware UseEndpoints trafia na koniec "pipe"
app.MapGet("/", () => "Hello World!");
app.MapGet("/{name}", (string name) => $"Hello {name}!");
app.MapGet("/bye", () => "Bye World!");


app.Run(async context =>
{
    await context.Response.WriteAsync("Under construction!");
});




app.Run();
