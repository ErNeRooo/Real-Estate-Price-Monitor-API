namespace repmAPI.Services
{
    public class DataService
    {
        public int GetAverage(List<int> prices)
        {
            return prices.Sum() / prices.Count;
        }
        public int GetMedian(List<int> prices)
        {
            return prices.Count % 2 == 0
                ? prices[prices.Count / 2] + prices[prices.Count / 2 - 1]
                : prices[prices.Count / 2];
        }
        public List<int> GetDominants(List<int> prices)
        {
            Dictionary<int, int> GroupedPrices = new Dictionary<int, int>();

            IEnumerable<IGrouping<int, int>> collectionsOfPrices = prices.GroupBy(price => price);
            foreach (var collection in collectionsOfPrices)
            {
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
