using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var webScraping = new WebScrapingSelenium();

        // Inicia o processo de scraping contínuo
        Task scrapingTask = webScraping.StartScraping();

        // Mantém o aplicativo em execução para o scraping contínuo
        await scrapingTask;
    }
}
