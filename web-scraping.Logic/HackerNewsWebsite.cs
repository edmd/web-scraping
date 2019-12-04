using System.Collections.Generic;
using HtmlAgilityPack;

namespace web_scraping.Logic
{
    // TODO: Implement Singleton pattern on this class and wrap the '_page' variable in MemCache,
    // TODO: populate only if cache expired
    public class HackerNewsWebsite
    {
        private readonly IPage _page;
        private readonly List<string> _importUrls;

        public HackerNewsWebsite()
        {
            // Load up all the news items in the 'Data' layer, filter on the 'UI'
            _page = new HackerNewsPage();
            _importUrls = new List<string>
            {
                "https://news.ycombinator.com/news?p=1",
                "https://news.ycombinator.com/news?p=2",
                "https://news.ycombinator.com/news?p=3",
                "https://news.ycombinator.com/news?p=4"
            };
        }
        
        public void ImportSite()
        {
            foreach (var importUrl in _importUrls)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load(importUrl);
                
                _page.IngestPage(htmlDoc);
            }
        }

        public string ReturnNewsItems(int count)
        {
            return _page.DeserializedWebScrapes(count);
        }
    }
}