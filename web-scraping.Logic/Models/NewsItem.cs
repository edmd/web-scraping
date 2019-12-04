using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace web_scraping.Logic.Models
{
    public class NewsItem
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }
        
        [JsonProperty("comments")]
        public int Comments { get; set; }
        
        [JsonProperty("rank")]
        public int Rank { get; set; }
    }
}