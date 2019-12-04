using EasyConsoleCore;

namespace web_scraping
{
    public class MainMenuPage : MenuPage
    {
        public MainMenuPage(Program program)
            : base("Main Page", program,
                new Option("Extract News", () => program.NavigateTo<WebScrapePage>()))
        {
            
        }
    }
}