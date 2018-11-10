using System.Collections.Generic;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Lunch.Data;
using Lunch.Menu;

namespace Lunch.Search
{
    public class MenuSearchEngine : ISearchEngine<MenuItem>
    {
        private const LuceneVersion LUCENE_VERSION = LuceneVersion.LUCENE_48;
        private const string DESCRIPTION = "description";
        private const string SPECIAL_CHARACTERS = @"+-!(){}[]^""~*?:/\";

        private readonly IndexWriter writer;
        private readonly Analyzer analyzer;
        private readonly QueryParser parser;
        private readonly SearcherManager manager;

        private IDictionary<string, MenuItem> items;

        public MenuSearchEngine()
        {
            analyzer = new StandardAnalyzer(LUCENE_VERSION, StandardAnalyzer.STOP_WORDS_SET);
            parser = new QueryParser(LUCENE_VERSION, DESCRIPTION, analyzer);

            var directory = System.IO.Directory.CreateDirectory(Settings.SearchIndexPath);

            writer = new IndexWriter(FSDirectory.Open(Settings.SearchIndexPath), new IndexWriterConfig(LUCENE_VERSION, analyzer));
            manager = new SearcherManager(writer, true, null);
        }

        public void SetDataset(IEnumerable<MenuItem> dataset)
        {
            items = new Dictionary<string, MenuItem>();

            dataset.ForEach(item =>
            {
                items.Add(item.Description, item);
                writer.UpdateDocument(new Term(DESCRIPTION, item.Description), new Document()
                {
                    new TextField(DESCRIPTION, item.Description, Field.Store.YES)
                });
            });             

            writer.Flush(true, true);
            writer.Commit();
        }

        public IEnumerable<MenuItem> Search(string queryString, ResultFilter filter = null)
        {
            if (queryString != null) queryString = queryString.Trim();
            if (string.IsNullOrEmpty(queryString)) yield break;
            queryString = Escape(queryString);
            queryString += '~'; 

            Query query = parser.Parse(queryString);
            manager.MaybeRefreshBlocking();
            IndexSearcher searcher = manager.Acquire();

            try
            {
                TopDocs documents = searcher.Search(query, (filter ?? ResultFilter.Default).Limit > 0 ? filter.Limit : 1);
                foreach (ScoreDoc scoreDocument in documents.ScoreDocs)
                {
                    Document document = searcher.Doc(scoreDocument.Doc);
                    yield return items[document.GetField(DESCRIPTION).GetStringValue()];
                }
            }
            finally
            {
                manager.Release(searcher);
                searcher = null;
            }
        }
        
        private string Escape(string query)
        {
            string result = "";

            foreach (char c in query)
            {
                // if (SPECIAL_CHARACTERS.Contains(c)) result += '\\';
                // result += c;

                if (!SPECIAL_CHARACTERS.Contains(c)) result += c;
            }

            return result;
        }
    }
}