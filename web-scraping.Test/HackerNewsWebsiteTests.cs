using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using web_scraping.Logic;
using web_scraping.Logic.Models;

namespace web_scraping.Test
{
    [TestFixture]
    public class HackerNewsWebsiteTests
    {
        //private Mock<HackerNewsWebsite> _hackerNewsWebsiteMock;
        private HackerNewsWebsite _hackerNewsWebsite;
        
        [SetUp]
        public void Setup()
        {
            //_hackerNewsWebsiteMock = new Mock<HackerNewsWebsite>();
            _hackerNewsWebsite = HackerNewsWebsite.Instance;
        }

        [Test]
        public void OneValidNewsItemTest()
        {
            const int noOfNewsItems = 1;
            _hackerNewsWebsite.ImportSite();
            var newsItemsString = _hackerNewsWebsite.ReturnNewsItems(noOfNewsItems);
            var newsItems = JsonConvert.DeserializeObject<List<NewsItem>>(newsItemsString);
            
            Assert.IsTrue(newsItems.Count() == noOfNewsItems, "One and only one item should be returned from ReturnNewsItems()");
            
            Assert.NotZero(newsItems[0].Comments, "NewsItem.Comments should not be zero");
            Assert.NotZero(newsItems[0].Points, "NewsItem.Points should not be zero");
            Assert.NotZero(newsItems[0].Rank, "NewsItem.Rank should not be zero");
            Assert.IsNotEmpty(newsItems[0].Author, "NewsItem.Comments should not be empty");
            Assert.IsNotEmpty(newsItems[0].Title, "NewsItem.Title should not be empty");
            Assert.IsNotEmpty(newsItems[0].Url.ToString(), "NewsItem.Url should not be empty");
            
            Assert.IsTrue(newsItems[0].Url.IsWellFormedOriginalString(), "NewsItem.Url is not well-formed");
        }
        
        [Test]
        public void MaximumNewsItemTest()
        {
            const int noOfNewsItems = 100;
            _hackerNewsWebsite.ImportSite();
            var newsItemsString = _hackerNewsWebsite.ReturnNewsItems(noOfNewsItems);
            var newsItems = JsonConvert.DeserializeObject<List<NewsItem>>(newsItemsString);
            
            Assert.IsTrue(newsItems.Count() == noOfNewsItems, "100 news Items should be returned from ReturnNewsItems()");
        }
    }
}