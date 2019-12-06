using System;
using HtmlAgilityPack;
using Moq;
using NUnit.Framework;
using web_scraping.Logic;
using web_scraping.Logic.Models;

namespace web_scraping.Test
{
    [TestFixture]
    public class HackerNewsPageTests
    {
        private Mock<HackerNewsPage> _hackerPageMock;
        private HackerNewsPage _hackerPage;

        [SetUp]
        public void Setup()
        {
            _hackerPageMock = new Mock<HackerNewsPage>();
        }

        [Test]
        public void ValidIngestPageTest()
        {
            _hackerPageMock.Setup(x => x.IngestPage(It.IsAny<HtmlDocument>())).Returns(true);
            _hackerPage = _hackerPageMock.Object;
            Assert.IsTrue(_hackerPage.IngestPage(new HtmlDocument()), "IngestPage failed.");
        }

        [Test]
        public void InvalidIngestPageTest()
        {
            _hackerPageMock.Setup(x => x.IngestPage(null)).Returns(false);
            _hackerPage = _hackerPageMock.Object;
            Assert.IsFalse(_hackerPage.IngestPage(null), "IngestPage failed.");
        }

        [Test]
        public void ValidDeserializedWebScrapesTest()
        {
            _hackerPageMock.Setup(x => x.DeserializedWebScrapes(0)).Returns("");
            _hackerPage = _hackerPageMock.Object;
            Assert.IsEmpty(_hackerPage.DeserializedWebScrapes(0), "DeserializedWebScrapes failed.");
        }

        [Test]
        public void InvalidDeserializedWebScrapesTest()
        {
            _hackerPageMock.Setup(x => x.DeserializedWebScrapes(1)).Returns("[]");
            _hackerPage = _hackerPageMock.Object;
            Assert.IsNotEmpty(_hackerPage.DeserializedWebScrapes(1), "DeserializedWebScrapes failed.");
        }

        [Test]
        public void ValidExtractAuthorTest()
        {
            _hackerPageMock.Setup(
                    x => x.ExtractAuthor(It.IsAny<HtmlNodeCollection>(), It.IsAny<int>(), It.IsAny<NewsItem>()))
                .Returns(new NewsItem());
            _hackerPage = _hackerPageMock.Object;

            var newsItem = _hackerPage.ExtractAuthor(new HtmlNodeCollection(null), 1, new NewsItem());
            Assert.IsNotNull(newsItem, "An empty NewsItem has been returned.");
        }

        [Test]
        public void InvalidExtractAuthorTest()
        {
            _hackerPageMock.Setup(
                    x => x.ExtractAuthor(null, It.IsAny<int>(), It.IsAny<NewsItem>()))
                .Throws(new Exception());
            _hackerPage = _hackerPageMock.Object;

            var ex = Assert.Throws<Exception>(() =>
                    _hackerPage.ExtractAuthor(null, 1, new NewsItem()),
                "Exception was not thrown for empty HtmlNodeCollection.");
        }

        [Test]
        public void ValidExtractCommentsTest()
        {
            _hackerPageMock.Setup(
                    x => x.ExtractComments(It.IsAny<HtmlNodeCollection>(), It.IsAny<int>(), It.IsAny<NewsItem>()))
                .Returns(new NewsItem());
            _hackerPage = _hackerPageMock.Object;

            var newsItem = _hackerPage.ExtractComments(new HtmlNodeCollection(null), 1, new NewsItem());
            Assert.IsNotNull(newsItem, "An empty NewsItem has been returned.");
        }

        [Test]
        public void InvalidExtractCommentsTest()
        {
            _hackerPageMock.Setup(
                    x => x.ExtractComments(null, It.IsAny<int>(), It.IsAny<NewsItem>()))
                .Throws(new Exception());
            _hackerPage = _hackerPageMock.Object;

            var ex = Assert.Throws<Exception>(() =>
                    _hackerPage.ExtractComments(null, 1, new NewsItem()),
                "Exception was not thrown for empty HtmlNodeCollection.");
        }

        [Test]
        public void ValidExtractPointsTest()
        {
            _hackerPageMock.Setup(
                    x => x.ExtractPoints(It.IsAny<HtmlNodeCollection>(), It.IsAny<int>(), It.IsAny<NewsItem>()))
                .Returns(new NewsItem());
            _hackerPage = _hackerPageMock.Object;

            var newsItem = _hackerPage.ExtractPoints(new HtmlNodeCollection(null), 1, new NewsItem());
            Assert.IsNotNull(newsItem, "An empty NewsItem has been returned.");
        }

        [Test]
        public void InvalidExtractPointsTest()
        {
            _hackerPageMock.Setup(
                    x => x.ExtractPoints(null, It.IsAny<int>(), It.IsAny<NewsItem>()))
                .Throws(new Exception());
            _hackerPage = _hackerPageMock.Object;

            var ex = Assert.Throws<Exception>(() =>
                    _hackerPage.ExtractPoints(null, 1, new NewsItem()),
                "Exception was not thrown for empty HtmlNodeCollection.");
        }

        [Test]
        public void ValidExtractRankTest()
        {
            _hackerPageMock.Setup(
                    x => x.ExtractRank(It.IsAny<HtmlNodeCollection>(), It.IsAny<int>(), It.IsAny<NewsItem>()))
                .Returns(new NewsItem());
            _hackerPage = _hackerPageMock.Object;

            var newsItem = _hackerPage.ExtractRank(new HtmlNodeCollection(null), 1, new NewsItem());
            Assert.IsNotNull(newsItem, "An empty NewsItem has been returned.");
        }

        [Test]
        public void InvalidExtractRankTest()
        {
            _hackerPageMock.Setup(
                    x => x.ExtractRank(null, It.IsAny<int>(), It.IsAny<NewsItem>()))
                .Throws(new Exception());
            _hackerPage = _hackerPageMock.Object;

            var ex = Assert.Throws<Exception>(() =>
                    _hackerPage.ExtractRank(null, 1, new NewsItem()),
                "Exception was not thrown for empty HtmlNodeCollection.");
        }

        [Test]
        public void ValidExtractTitleTest()
        {
            _hackerPageMock.Setup(
                    x => x.ExtractTitle(It.IsAny<HtmlNodeCollection>(), It.IsAny<int>(), It.IsAny<NewsItem>()))
                .Returns(new NewsItem());
            _hackerPage = _hackerPageMock.Object;

            var newsItem = _hackerPage.ExtractTitle(new HtmlNodeCollection(null), 1, new NewsItem());
            Assert.IsNotNull(newsItem, "An empty NewsItem has been returned.");
        }

        [Test]
        public void InvalidExtractTitleTest()
        {
            _hackerPageMock.Setup(
                    x => x.ExtractTitle(null, It.IsAny<int>(), It.IsAny<NewsItem>()))
                .Throws(new Exception());
            _hackerPage = _hackerPageMock.Object;

            var ex = Assert.Throws<Exception>(() =>
                    _hackerPage.ExtractTitle(null, 1, new NewsItem()),
                "Exception was not thrown for empty HtmlNodeCollection.");
        }

        [Test]
        public void ValidExtractUrlTest()
        {
            _hackerPageMock.Setup(
                    x => x.ExtractUrl(It.IsAny<HtmlNodeCollection>(), It.IsAny<int>(), It.IsAny<NewsItem>()))
                .Returns(new NewsItem());
            _hackerPage = _hackerPageMock.Object;

            var newsItem = _hackerPage.ExtractUrl(new HtmlNodeCollection(null), 1, new NewsItem());
            Assert.IsNotNull(newsItem, "An empty NewsItem has been returned.");
        }

        [Test]
        public void InvalidExtractUrlTest()
        {
            _hackerPageMock.Setup(
                    x => x.ExtractUrl(null, It.IsAny<int>(), It.IsAny<NewsItem>()))
                .Throws(new Exception());
            _hackerPage = _hackerPageMock.Object;

            var ex = Assert.Throws<Exception>(() =>
                    _hackerPage.ExtractUrl(null, 1, new NewsItem()),
                "Exception was not thrown for empty HtmlNodeCollection.");
        }
    }
}