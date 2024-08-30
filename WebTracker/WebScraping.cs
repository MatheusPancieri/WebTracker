using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading.Tasks;

public class WebScrapingSelenium
{
    private string _lastPrice;

    public async Task<string> ScrapeDataWithSelenium()
    {
        // Inicializa o ChromeDriver
        using (IWebDriver driver = new ChromeDriver())
        {
            try
            {
                // Navega até a página do produto
                driver.Navigate().GoToUrl("https://www.br.vaio.com/notebook-vaio-fe15-amd-ryzen-5-5500u-linux-16gb-ram-256gb-ssd-15-6-full-hd-cinza-grafite-3344208/p");

                // Aguarda o carregamento do elemento de preço (por exemplo, 10 segundos)
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(drv => drv.FindElement(By.CssSelector(".vaiobr-loja-2-x-wrapper__preco_por_produto")));

                // Captura o elemento de preço
                var priceElement = driver.FindElement(By.CssSelector(".vaiobr-loja-2-x-wrapper__preco_por_produto"));
                var price = priceElement.Text.Trim();

                Console.WriteLine("Preço: " + price);
                return price;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao capturar o preço: {ex.Message}");
                return null;
            }
        }
    }

    public async Task StartScraping()
    {
        while (true)
        {
            // Aguarda a execução do método ScrapeDataWithSelenium
            var currentPrice = await ScrapeDataWithSelenium();

            if (currentPrice != null && currentPrice != _lastPrice)
            {
                _lastPrice = currentPrice;
                await SendPriceChangeEmail(currentPrice);
            }

            // Atraso para a próxima verificação. Ajuste para 1 dia
            await Task.Delay(TimeSpan.FromDays(1)); // Aguarda 1 dia
        }
    }

    private async Task SendPriceChangeEmail(string newPrice)
    {
        var emailNotification = new EmailNotification();
        await emailNotification.EnviarEmail(newPrice); // Passa o novo preço para o método EnviarEmail
        Console.WriteLine($"E-mail enviado com o novo preço: {newPrice}");
    }
}
