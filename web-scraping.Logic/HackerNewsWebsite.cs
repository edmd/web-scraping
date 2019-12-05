using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;

namespace web_scraping.Logic
{
    public sealed class HackerNewsWebsite
    {
        private  IPage _page;
        private static HackerNewsWebsite instance;
        MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        static HackerNewsWebsite() { }

        private HackerNewsWebsite() { }

        public static HackerNewsWebsite Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HackerNewsWebsite();
                }
                return instance;
            }
        }
        
        public void ImportSite()
        {
            if (cache.TryGetValue("HackerNewsPage", out IPage page))
            {
                _page = page;
            }
            else
            {
                _page = new HackerNewsPage();

                // Load up all the news items in the 'Data' layer, filter on the 'UI'
                var importUrls = new List<string>
                {
                    "https://news.ycombinator.com/news?p=1",
                    "https://news.ycombinator.com/news?p=2",
                    "https://news.ycombinator.com/news?p=3",
                    "https://news.ycombinator.com/news?p=4"
                };

                foreach (var importUrl in importUrls)
                {
                    var web = new HtmlWeb();
                    var htmlDoc = web.Load(importUrl);

                    _page.IngestPage(htmlDoc);
                }
                
                var entry = cache.CreateEntry("HackerNewsPage");
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20);
                entry.SetValue(_page);
                entry.Dispose();
            }
        }

        public string ReturnNewsItems(int count)
        {
            return _page.DeserializedWebScrapes(count);
        }
    }
}