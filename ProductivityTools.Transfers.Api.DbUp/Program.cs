using DbUp;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using ProductivityTools.MasterConfiguration;

IConfigurationRoot configuration =
                new ConfigurationBuilder()
                .AddMasterConfiguration(configName: "ProductivityTools.Transfers.WebApi.json", force: true)
                .Build();

var masterConnectionString = configuration["ConnectionString"];
var connectionString = args.FirstOrDefault() ?? masterConnectionString; 
EnsureDatabase.For.SqlDatabase(connectionString);
Console.WriteLine(connectionString);
Console.WriteLine("pawel");

var upgrader =
    DeployChanges.To
        .SqlDatabase(connectionString)
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        .LogToConsole()
        .WithExecutionTimeout(TimeSpan.FromSeconds(3600))
        .Build();

var result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
#if DEBUG
    Console.ReadLine();
#endif
    return -1;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success!");
Console.ResetColor();
return 0;
