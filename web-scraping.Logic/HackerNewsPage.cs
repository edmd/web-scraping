using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Newtonsoft.Json;
using web_scraping.Logic.Models;

namespace web_scraping.Logic
{
    public class HackerNewsPage : IPage
    {
        private List<NewsItem> _newsItems;

        public HackerNewsPage()
        {
            _newsItems = new List<NewsItem>();
            ParseErrors = new List<string>();
        }
        
        public void IngestPage(HtmlDocument document)
        {
            try
            {
                var tableNode = document.DocumentNode.SelectNodes("//table[@class='itemlist']").First();
                if (tableNode == null)
                {
                    this.LogInfo("Missing news data.");
                    return;
                }
                
                var trNodes = tableNode.SelectNodes(".//tr");
                const bool validEntry = true; // We'll add the entries even if some values are empty
                
                for (var i = 0; i <= 87; i += 3) // 30 items to a news page
                {
                    var newsItem = new NewsItem();
                    
                    newsItem = ExtractRank(trNodes, i, newsItem);
                    
                    newsItem = ExtractUrl(trNodes, i, newsItem);

                    newsItem = ExtractTitle(trNodes, i, newsItem);

                    newsItem = ExtractPoints(trNodes, i, newsItem);

                    newsItem = ExtractAuthor(trNodes, i, newsItem);

                    newsItem = ExtractComments(trNodes, i, newsItem);

                    if (validEntry)
                    {
                        _newsItems.Add(newsItem);
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogInfo("General exception occurred during parsing: " + ex.Message);
            }
        }

        public string DeserializedWebScrapes(int count)
        {
            return JsonConvert.SerializeObject(_newsItems.GetRange(0, count), Formatting.Indented);
        }
        
        #region Extraction methods...
        public NewsItem ExtractComments(HtmlNodeCollection trNodes, int i, NewsItem newsItem)
        {
            var commentsString = (string) null;
            try
            {
                // comments
                commentsString = trNodes[i + 1].SelectNodes(".//a")[3].InnerText;
                int.TryParse(Regex.Replace(commentsString, "[^0-9]", ""), out var comments);
                newsItem.Comments = comments;
            }
            catch (Exception)
            {
                this.LogInfo("Error parsing 'comments': " + commentsString);
            }
            
            return newsItem;
        }

        public NewsItem ExtractAuthor(HtmlNodeCollection trNodes, int i, NewsItem newsItem)
        {
            var authorString = (string) null;
            try
            {
                // author
                authorString = trNodes[i + 1].SelectNodes(".//a[@class='hnuser']").First().InnerText;
                if (authorString.Length > 256)
                {
                    authorString = authorString.Substring(0, 256);
                }
                newsItem.Author = authorString;
            }
            catch (Exception)
            {
                this.LogInfo("Error parsing 'author': " + authorString);
                newsItem.Author = "n/a"; // This news entry does not have an author
            }
            
            return newsItem;
        }

        public NewsItem ExtractPoints(HtmlNodeCollection trNodes, int i, NewsItem newsItem)
        {
            var pointsString = (string) null;
            try
            {
                // points
                pointsString = trNodes[i + 1].SelectNodes(".//span[@class='score']").First().InnerText;
                int.TryParse(Regex.Replace(pointsString, "[^0-9]", ""), out var points);
                newsItem.Points = points;
            }
            catch (Exception)
            {
                this.LogInfo("Error parsing 'points': " + pointsString);
            }
            
            return newsItem;
        }

        public NewsItem ExtractTitle(HtmlNodeCollection trNodes, int i, NewsItem newsItem)
        {
            var title = (string) null;
            try
            {
                // title
                title = trNodes[i].SelectNodes(".//a[@class='storylink']").First().InnerText;
                if (title.Length > 256)
                {
                    title = title.Substring(0, 256);
                }

                newsItem.Title = title;
            }
            catch (Exception)
            {
                this.LogInfo("Error parsing 'title': " + title);
                newsItem.Title = "n/a"; // This news entry does not have a title
            }
            
            return newsItem;
        }

        public NewsItem ExtractUrl(HtmlNodeCollection trNodes, int i, NewsItem newsItem)
        {
            var url = (string) null;
            try
            {
                // uri
                url = trNodes[i].SelectNodes(".//a[@class='storylink']").First().Attributes[0].Value;
                if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                {
                    newsItem.Url = new Uri(url);
                }
                else
                {
                    this.LogInfo("Malformed Url for 'uri'. Value: " + url);
                }
            }
            catch (Exception)
            {
                this.LogInfo("The format of the Url could not be determined:. Url: " + url);
            }

            return newsItem;
        }

        public NewsItem ExtractRank(HtmlNodeCollection trNodes, int i, NewsItem newsItem)
        {
            var rankString = (string)null;
            try
            {
                // rank
                rankString = trNodes[i].SelectNodes(".//span[@class='rank']").First().InnerText;
                if (!int.TryParse(Regex.Replace(rankString, "[^0-9]", ""), out var rank))
                {
                    this.LogInfo("Could not parse string for 'rank'. Value: " + rankString);
                }

                newsItem.Rank = rank;
            }
            catch (Exception)
            {
                this.LogInfo("Could not parse string for 'rank'. Value: " + rankString);
                //validEntry = false;
            }
            
            return newsItem;
        }
        #endregion
        
        // TODO: Implement logging framework in the config with Log4Net
        #region Logging...
        private List<string> ParseErrors;
        private void LogInfo(string message)
        {
            ParseErrors.Add(message);
        }
        #endregion
    }
}