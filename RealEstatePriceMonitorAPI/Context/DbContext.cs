using Microsoft.Azure.Cosmos;
using RealEstatePriceMonitor.Classes;
using System.Data.Common;

namespace RealEstatePriceMonitor.Context
{
    public class DbContext
    {
        private const string connectionString = "AccountEndpoint=https://database-account.documents.azure.com:443/;AccountKey=tDOtc3bA9WpS5hB9e0pN82ozdR64bNLeZIcBwj1xOo6B1KbSRVuR5qX9Lx2TuRSx7Zx9nQHjh0PYACDbGFfglg==;";
        private CosmosClient cosmosClient { get; set; }
        public DbContext() {
            cosmosClient = new CosmosClient(connectionString);
        }

        public async void add(PriceDataRecord data)
        {
            Container container = cosmosClient.GetContainer("RealEstatePrices", "Prices");

            await container.CreateItemAsync(data);
        }
    }
}
