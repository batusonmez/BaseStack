using Microsoft.Extensions.DependencyInjection;
using System;
using System.CommandLine;

namespace Person.Infrastructure.CLI
{
    public static class CLIExtension
    {
        public static void AddCLI(this IServiceCollection services, string[] args)
        {
            var rootCommand = new RootCommand("Person API Command Line API");
            var commands = services.BuildServiceProvider().GetServices<Command>().ToList();

            foreach (var command in commands)
            {
                rootCommand.AddCommand(command);
            }

              rootCommand.InvokeAsync(args).Wait();
        }
    }
}
