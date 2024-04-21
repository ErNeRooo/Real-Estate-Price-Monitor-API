using HtmlAgilityPack;
using System;

namespace repmAPI.Context
{
    public class ScrapingContext
    {
        private string Url = "https://warszawa.nieruchomosci-online.pl/szukaj.html?3,mieszkanie,wynajem,,Olsztyn:18670&o=price,desc";
        private HtmlDocument HtmlDocument;
        public ScrapingContext()
        {
            LoadDocument(Url);
        }
        private void LoadDocument(string url)
        {
            var client = new HttpClient();
            var html = client.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            HtmlDocument = htmlDocument;
        }
        public List<int> GetPrices()
        {
            int numberOfOffers =Convert.ToInt32(HtmlDocument.DocumentNode.SelectSingleNode("//span[@id='boxOfCounter']").InnerText.Replace(" ogłoszenia", ""));
            var priceElements = HtmlDocument.DocumentNode.SelectNodes("//p[@class='title-a primary-display']/span[1]");

            for(int pageNumber = 2; priceElements.Count() < numberOfOffers; pageNumber++)
            {
                LoadDocument(Url + "&p=" + pageNumber.ToString());
                var priceElementsFromNextPage = HtmlDocument.DocumentNode.SelectNodes("//p[@class='title-a primary-display']/span[1]");

                foreach(var priceElementFromNextPage in priceElementsFromNextPage)
                {
                    priceElements.Add(priceElementFromNextPage);

                    if (priceElements.Count() >= numberOfOffers) break;
                }

            }

            var prices = new List<int>();

            foreach (var priceElement in priceElements)
            {
                string priceString = priceElement.InnerText.Replace("&nbsp;", "").Replace("zł", "");
                int price = Convert.ToInt32(priceString);
                prices.Add(price);
            }
            prices.Sort();
            return prices;
        }

    }
}
