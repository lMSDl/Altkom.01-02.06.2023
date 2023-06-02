var builder = WebApplication.CreateBuilder(args);
//ConfigureServices
builder.Services.AddTransient<object>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    //Configure
    var @object = app.Services.GetService<object>();
    Console.WriteLine(app.Services.GetService<IConfiguration>()["MyData"]);

}




app.Run();
