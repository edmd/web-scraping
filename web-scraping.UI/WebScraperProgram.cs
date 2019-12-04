using System;
using EasyConsoleCore;

namespace web_scraping
{
    public class HackerNewsProgram : Program
    {
        public HackerNewsProgram()
            : base("Web scraper", false)
        {
            AddPage(new MainMenuPage(this));
            AddPage(new WebScrapePage(this));

            SetPage<MainMenuPage>();
        }
    }
}