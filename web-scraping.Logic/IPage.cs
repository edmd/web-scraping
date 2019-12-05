using HtmlAgilityPack;

namespace web_scraping.Logic
{
    public interface IPage
    {
        string DeserializedWebScrapes(int count);
        bool IngestPage(HtmlDocument document);
    }
}