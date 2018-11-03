using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Lunch.Menu.Kafetaria
{
    public class KafetariaScraper
    {
        private const char Whitespace = (char) 160;
        private const char Slash = '/';

        private IEnumerable<string> Urls = new List<string>
        {
            "http://www.kafetaria.nl/verse-belegde-broodjes-in-rijnsburg/"
        };

        public Menu Scrape() 
        {
            Menu menu = new Menu();

            {
                HtmlDocument document = new HtmlWeb().Load("http://www.kafetaria.nl/verse-belegde-broodjes-in-rijnsburg/");
                Menu sandwiches = ScrapeSandwiches(document.DocumentNode);
                if (sandwiches != null) menu.AddMenu(sandwiches);
            }

            return menu;
        }

        // in order to scrape one of the most inconsistent websites ever
        // this shitload of ugly code is needed
        private Menu ScrapeSandwiches(HtmlNode root)
        {
            IEnumerable<HtmlNode> rows = root
                .Descendants("table")
                .Where(node => node.Attributes["id"].Value == "tabel3")
                .First()
                .Descendants("tr")
                .Where(node =>
                    node.ChildNodes.Where(n => n.Name == "td").Count() > 1 &&
                    node.ChildNodes.Any(n => !string.IsNullOrEmpty(n.InnerText.Trim(' ', '\r', '\n')))
                );

            if (!rows.Any()) return null;

            Menu menu = new Menu();
            IDictionary<int, IEnumerable<string>> buns = null;
            foreach (HtmlNode row in rows)
            {
                int index = 0;
                IEnumerable<HtmlNode> columns = row.Descendants("td");

                if (buns == null)
                {
                    buns = new Dictionary<int, IEnumerable<string>>();
                    
                    menu.Title = columns.First().InnerText.Trim();

                    {
                        HtmlNode column = columns.Skip(++index).FirstOrDefault();
                        string text = column.InnerText.Trim();

                        IEnumerable<string> parts = text.Split(Whitespace);
                        string base_ = string.Join(" ", parts.TakeWhile(p => !p.Contains(Slash)));

                        buns.Add(index, parts
                            .Where(p => p.Contains(Slash))
                            .SelectMany(p => p.Split(Slash))
                            .Select(p => $"{base_} {p}"));
                    }

                    {
                        HtmlNode column = columns.Skip(++index).FirstOrDefault();
                        buns.Add(index, column.InnerText
                            .Trim()
                            .Split(Whitespace)
                            .Where(p => !string.IsNullOrEmpty(p)));
                    }

                    continue;
                }

                string sandwich = columns.First().InnerText.Trim();

                for (index = 1; index < columns.Count(); index++)
                {
                    if (decimal.TryParse(columns.Skip(index).First().InnerText.Trim(), out decimal price))
                    {
                        buns[index]
                            .Select(bun => $"{bun} {sandwich}")
                            .ForEach(description => menu.AddItem(description, price));
                    }
                }
            }

            return menu;
        }
    }
}