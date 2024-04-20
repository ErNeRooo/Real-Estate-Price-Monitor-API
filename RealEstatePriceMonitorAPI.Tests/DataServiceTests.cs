using repmAPI.Services;

namespace RealEstatePriceMonitorAPI.Tests
{
    public class DataServiceTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void GetAverage_ForGivenNumbers_ReturnCorrectAverage(List<int> prices, int expectedResult)
        {
            //arrange
            var dataService = new DataService();

            //act
            int result = dataService.GetAverage(prices);

            //assert
            Assert.Equal(expectedResult, result);
        }


        public static IEnumerable<object[]> Data()
        {
            yield return new object[] { new List<int>() { 2, 3, 4 }, 3 };
            yield return new object[] { new List<int>() { 5, 10, 15 }, 10 };
            yield return new object[] { new List<int>() { 3, 6, 9 }, 6 };
        }
        
    }
}