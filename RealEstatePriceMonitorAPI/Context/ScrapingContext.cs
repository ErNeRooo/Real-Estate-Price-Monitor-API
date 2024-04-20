﻿using HtmlAgilityPack;
using System;

namespace repmAPI.Context
{
    public class ScrapingContext
    {
        private string url = "https://warszawa.nieruchomosci-online.pl/szukaj.html?3,mieszkanie,wynajem,,Olsztyn:18670&o=price,desc";
        private HtmlDocument HtmlDocument;
        public ScrapingContext()
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

    }
}