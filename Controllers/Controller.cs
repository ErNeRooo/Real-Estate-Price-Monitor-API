using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;

namespace repmAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private string url = "https://warszawa.nieruchomosci-online.pl/szukaj.html?3,mieszkanie,wynajem,,Olsztyn:18670&o=price,desc";

        [HttpGet()] 
        public ActionResult GetAverage()
        {
            var client = new HttpClient();
            var html = client.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var pricesElements = htmlDocument.DocumentNode.SelectNodes("//p[@class='title-a primary-display']/span[1]");

            var prices = new List<int>();

            foreach ( var priceElement in pricesElements )
            {
                string priceString = priceElement.InnerText.Replace("&nbsp;", "").Replace("zł", "");
                int price = Convert.ToInt32(priceString);
                prices.Add(price);
            }

            int pricesAverage = prices.Sum() / prices.Count;

            return Ok(pricesAverage);
        }
    }
}