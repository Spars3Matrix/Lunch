using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Lunch.Data;
using Lunch.Menu;
using Lunch.Search;
using Microsoft.Extensions.Configuration;

namespace Lunch.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Configure();

            SlackSimulator simulator = new SlackSimulator();
            simulator.PrintWelcomeMessage();
            simulator.SetInitiator();
            do 
            {
                simulator.ExcecuteCommand();
            }
            while (true);
        }

        private static void Configure()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            Settings.Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.Development.json", false, true)
                .Build();

            new MenuService()
                .SetMenuProvider(new KafetariaMenuProvider())
                .SetSearchEngine(new MenuSearchEngine());
        }
    }
}
