using HtmlAgilityPack;
using System;

namespace repmAPI.Services
{
    public class ScrapingService
    {
        private string url = "https://warszawa.nieruchomosci-online.pl/szukaj.html?3,mieszkanie,wynajem,,Olsztyn:18670&o=price,desc";
        private HtmlDocument HtmlDocument;
        public ScrapingService() 
        {
            LoadDocument();
        }
        private void LoadDocument()
        {
            var client = new HttpClient();
            var html = client.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            HtmlDocument = htmlDocument;
        }
        public List<int> GetPrices()
        {
            var priceElements = HtmlDocument.DocumentNode.SelectNodes("//p[@class='title-a primary-display']/span[1]");

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
        public int GetAverage()
        {
            var prices = GetPrices();
            return prices.Sum() / prices.Count;
        }
        public int GetMedian()
        {
            var prices = GetPrices();

            return prices.Count % 2 == 0 
                ? prices[prices.Count / 2] + prices[prices.Count / 2 - 1]  
                : prices[prices.Count / 2];
        }
        public List<int> GetDominants()
        {
            Dictionary<int, int> GroupedPrices = new Dictionary<int, int>();

            IEnumerable<IGrouping<int, int>> collectionsOfPrices = GetPrices().GroupBy(price => price);
            foreach (var collection in collectionsOfPrices) {
                GroupedPrices.Add(collection.Key, collection.Count());
            }

            List<int> dominants = new List<int>();

            var maxValue = GroupedPrices.Values.Max();
            foreach (var pair in GroupedPrices)
            {
                if (pair.Value == maxValue) dominants.Add(pair.Key);
            }

            return dominants;
        }
    }
}
