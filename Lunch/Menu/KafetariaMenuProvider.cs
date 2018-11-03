using Lunch.Menu.Kafetaria;

namespace Lunch.Menu
{
    public class KafetariaMenuProvider : MenuProvider
    {
        protected override Menu CreateMenu()
        {
            return new KafetariaScraper().Scrape();
        }
    }
}