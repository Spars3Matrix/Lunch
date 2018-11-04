using System;
using System.IO;
using Lunch.Data;
using Lunch.Menu;
using Lunch.Search;
using Microsoft.Extensions.Configuration;

namespace Lunch.Tests
{
    public abstract class BaseTest : IDisposable
    {
        public BaseTest() 
        {
            Settings.Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../../../Lunch.Api"))
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.Development.json", false, true)
                .Build();

            LunchDatabase.IsTesting = true;
            new MenuService()
                .SetMenuProvider(new MockMenuProvider())
                .SetSearchEngine(new SimpleSearchEngine<MenuItem>());
            Initialize();
        }

        public void Dispose()
        {
            Cleanup();
        }

        protected virtual void Initialize() {}
        protected virtual void Cleanup()
        {
            // cleaning up test data between tests
            using (LunchDatabase database = new LunchDatabase())
            {
                database.OrderItems.RemoveRange(database.OrderItems);
                database.SaveChanges();
            }
        }
    }
}