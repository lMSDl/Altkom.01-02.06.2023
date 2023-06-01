using ConsoleApp.Config.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;



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
