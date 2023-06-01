// See https://aka.ms/new-console-template for more information

using BsCli.Commands.Index;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.CommandLine;


class Program
{
    static async Task<int> Main(string[] args)
    {
        var host = CreateDefaultBuilder().Build();

        host.Run();

        var rootCommand = new RootCommand("Base Stack Command Line API");
        
        new IndexCommand(rootCommand);


        return await rootCommand.InvokeAsync(args);
    }

    private static IHostBuilder CreateDefaultBuilder()
    {
        return Host.CreateDefaultBuilder()
                   .ConfigureAppConfiguration(app =>
                   {
                       app.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                   })
                   .ConfigureServices(services =>
                   {
                       // this is the line that has the issue
                       services.Configure<MailSettings>(services.Configuration.GetSection("MailSettings"));
                   });
    }
}