using ConsoleApp;
using ConsoleApp.Config.Models;
using ConsoleApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;

IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("Config/config.json")
                .Build() ;


var serviceCollection = new ServiceCollection();


serviceCollection.AddTransient<IOutputService>(provider => new DebugOutput());

//transient - zawsze nowa instancja
serviceCollection.AddTransient<IOutputService, ConsoleRandomFontOutput>();
//scoped - instacja wytwarzana dla każdego nowego scope
serviceCollection.AddScoped<IFontService, StandardFontService>();
//singleton - instancja tworzona tylko raz
serviceCollection.AddSingleton<IFontService, KeyboardSmallFontService>();


serviceCollection.AddSingleton<ConsoleOutput>();
serviceCollection.AddTransient<IOutputService>(provider => provider.GetService<ConsoleOutput>()!);

serviceCollection.AddLogging(options => options.AddConfiguration(config.GetSection("Logging"))
                                               //.SetMinimumLevel(LogLevel.Debug)
                                               .ClearProviders()
                                               .AddConsole(/*x => x.IncludeScopes = true*/)
                                               .AddDebug()
                                               .AddEventLog());

serviceCollection.AddTransient<LoggerDemo>();


var serviceProvider = serviceCollection.BuildServiceProvider();


var logger = serviceProvider.GetService<ILogger<Program>>();

logger.LogInformation("Hello World!");
logger.LogDebug("Debug...");


serviceProvider.GetService<LoggerDemo>().Work();


DependencyInjection(serviceProvider);

Console.ReadLine();






static void Introduction()
{
    string? a = "ala";
    string b = ToUpper(a)!;


    string? ToUpper(string @string /*!! - null-checking feature - dodaje podczas kompilacji (niejawnie) kod jak poniżej*/)
    {
        /*    if (@string == null)
                throw new ArgumentNullException(nameof(@string));*/

        return @string?.ToUpper();
    }


    //namespace ConsoleApp
    //{
    //    internal class Program
    //    {
    //        static void Main()
    //        {
    //instrukcje najwyższego poziomu
    Console.WriteLine("Hello, World!");

    //       }
    //    }
    //}
}

static void Configuration()
{
    //Microsoft.Extensions.Configuration
    IConfiguration config = new ConfigurationBuilder()

                //Microsoft.Extensions.Configuration.Xml
                .AddXmlFile("Config/config.xml")

                //Microsoft.Extensions.Configuration.Ini
                .AddIniFile("Config/config.ini")

                //NetEscapades.Configuration.Yaml
                .AddYamlFile("Config/config.yaml", optional: true, reloadOnChange: true)

                //Microsoft.Extensions.Configuration.Json
                .AddJsonFile("Config/config.json", optional: true)

                //Microsoft.Extensions.Configuration.EnvironmentVariables
                .AddEnvironmentVariables()

                //w przypadku powtarzających się kluczy, zastosowanie ma ten ostatnio załadowany
                .Build();


    var greetigsSection = config.GetSection("Greetings");
    var targetsSection = greetigsSection.GetSection("Targets");
    Console.WriteLine($"{greetigsSection["Value"]} from {targetsSection["From"]} to {config["Greetings:Targets:To"]}");


    var greetings = new Greetings();
    //Microsoft.Extensions.Configuration.Binder
    greetigsSection.Bind(greetings);

    for (int i = 0; i < greetings.Repeat; i++)
    {
        Console.WriteLine($"{greetings.Value} from {greetings.Targets.From} to {greetings.Targets.To}");
    }

    greetings = config.GetSection(nameof(Greetings)).Get<Greetings>();
    Console.WriteLine($"{greetings.Value} from {greetings.Targets.From} to {greetings.Targets.To}");


    //for (int i = 0; i < int.Parse(config["Count"]); i++)
    for (int i = 0; i < config.GetValue<int>("Count"); i++)
    {

        Console.WriteLine($"Hello, {config["HelloJson"]}");
        Console.WriteLine($"Hello, {config["HelloXml"]}");
        Console.WriteLine($"Hello, {config["HelloIni"]}");
        Console.WriteLine($"Hello, {config["HelloYaml"]}");

        Console.ReadLine();

    }


    Console.WriteLine(config["tmp"]);
    Console.WriteLine(config["bAjKa"]);
}

static void DependencyInjection(ServiceProvider serviceProvider)
{
    serviceProvider.GetService<ConsoleOutput>()!.WriteText("abc");
    serviceProvider.GetService<ConsoleOutput>()!.WriteText("abc");
    serviceProvider.GetService<ConsoleOutput>()!.WriteText("abc");
    serviceProvider.GetService<ConsoleOutput>()!.WriteText("abc");

    IOutputService output = serviceProvider.GetService<IOutputService>()!;

    output.WriteText("Hello!");

    using (var scope = serviceProvider.CreateScope())
    {

        output = scope.ServiceProvider.GetService<IOutputService>()!;

        output.WriteText("Hello!");

        int counter = 0;
        foreach (var service in scope.ServiceProvider.GetServices<IOutputService>())
        {
            service.WriteText((++counter).ToString());
        }
    }

    using (var scope = serviceProvider.CreateScope())
    {
        output = scope.ServiceProvider.GetService<IOutputService>()!;
        output.WriteText("Hello!");
    }
}