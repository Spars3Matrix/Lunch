using System;
using Lunch.Data;
using Lunch.Menu;

namespace Lunch.Tests
{
    public abstract class BaseTest : IDisposable
    {
        public BaseTest() 
        {
            LunchDatabase.IsTesting = true;
            new MenuService().SetMenuProvider(new MockMenuProvider());
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