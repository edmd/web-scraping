using System;
using EasyConsoleCore;
using web_scraping.Logic;

namespace web_scraping
{
    public class WebScrapePage : Page
    {
        public WebScrapePage(Program program)
            : base("Extract feed", program)
        {
        }

        public override void Display()
        {
            base.Display();

            var feedCount = Input.ReadInt("Enter the number of posts to extract: ", 1, 100);
            Output.WriteLine($"Outputting {feedCount} post(s) from Hacker News website (https://news.ycombinator.com/news)");

            var website = HackerNewsWebsite.Instance;
            website.ImportSite();

            var newsString = website.ReturnNewsItems(feedCount);
            
            var lines = newsString.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );
            
            WriteLines(lines);

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
        
        private static void WriteLines(params string[] items)
        {
            foreach(var item in items)
            {
                Console.WriteLine(item);
            }
        }
    }
}