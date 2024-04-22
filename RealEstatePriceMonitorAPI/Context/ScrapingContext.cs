using HtmlAgilityPack;
using System;
using System.Text;

namespace repmAPI.Context
{
    public class ScrapingContext
    {
        private string Url = "https://warszawa.nieruchomosci-online.pl/szukaj.html?3,mieszkanie,wynajem,,CityName&o=price,desc";
        private HtmlDocument HtmlDocument;
        public ScrapingContext()
        {
            
        }
        private void LoadDocument(string url)
        {
            var client = new HttpClient();
            var html = client.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            HtmlDocument = htmlDocument;
        }
        public string removeAllCharsFromStringExceptNumbers(string givenString)
        {
            StringBuilder result = new StringBuilder();
            char[] allowedChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            foreach (char item in givenString)
            {
                if (allowedChars.Contains(item)) { result.Append(item); }
            }

            return result.ToString();
        }
        public List<int> GetPrices(string cityName)
        {
            Url = Url.Replace("CityName", cityName);
            LoadDocument(Url);

            var OffersElement = HtmlDocument.DocumentNode.SelectSingleNode("//span[@id='boxOfCounter']").InnerText;
            int numberOfOffers = Convert.ToInt32(removeAllCharsFromStringExceptNumbers(OffersElement));

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
                string priceString = removeAllCharsFromStringExceptNumbers(priceElement.InnerText);
                int price = Convert.ToInt32(priceString);
                prices.Add(price);
            }
            prices.Sort();
            return prices;
        }
    }
}
