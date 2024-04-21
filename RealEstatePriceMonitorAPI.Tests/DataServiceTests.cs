using repmAPI.Services;

namespace RealEstatePriceMonitorAPI.Tests
{
    public class DataServiceTests
    {
        [Theory]
        [MemberData(nameof(DataForCalculateAverage))]
        public void CalculateAverage_ForGivenNumbers_ReturnCorrectAverage(List<int> prices, int expectedResult)
        {
            //arrange
            var dataService = new DataService();

            //act
            int result = dataService.CalculateAverage(prices);

            //assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [MemberData(nameof(DataForCalculateMedian))]
        public void CalculateMedian_ForGivenNumbers_ReturnCorrectMedian(List<int> prices, int expectedResult)
        {
            //arrange
            var dataService = new DataService();

            //act
            int result = dataService.CalculateMedian(prices);

            //assert
            Assert.Equal(expectedResult, result);
        }
        
        [Theory]
        [MemberData(nameof(DataForCalculateDominants))]
        public void CalculateDominants_ForGivenNumbers_ReturnCorrectDominants(List<int> prices, List<int> expectedResult)
        {
            //arrange
            var dataService = new DataService();

            //act
            List<int> result = dataService.CalculateDominants(prices);

            //assert
            Assert.Equal(expectedResult, result);
        }

        public static IEnumerable<object[]> DataForCalculateAverage()
        {
            yield return new object[] { new List<int>() { 0 }, 0 };
            yield return new object[] { new List<int>() { 5 }, 5 };
            yield return new object[] { new List<int>() { 2, 3 }, 2 };
            yield return new object[] { new List<int>() { 3, 6, 9 }, 6 };
            yield return new object[] { new List<int>() { 5, 10, 15, 20 }, 12 };
            yield return new object[] { new List<int>() { 1, 1, 9, 2 }, 3 };
            yield return new object[] { new List<int>() { 4, 2, 8, 34, 2, 1923 }, 328 };
            yield return new object[] { new List<int>() { 1914, 1939, 2036, 0, 0, 0 }, 981 };
        }
        public static IEnumerable<object[]> DataForCalculateMedian()
        {
            yield return new object[] { new List<int>() { 0 }, 0 };
            yield return new object[] { new List<int>() { 2 }, 2 };
            yield return new object[] { new List<int>() { 2, 4 }, 3 };
            yield return new object[] { new List<int>() { 5, 10, 15 }, 10 };
            yield return new object[] { new List<int>() { 3, 6, 9 }, 6 };
            yield return new object[] { new List<int>() { 2, 4, 4, 4, 6 }, 4 };
            yield return new object[] { new List<int>() { 2, 4, 4, 2077, 4, 6 }, 4 };
            yield return new object[] { new List<int>() { 0, 4, 4, 2077, 20, 6 }, 5 };
            yield return new object[] { new List<int>() { 1, 2, 1233124, 141414, 4 , 13 }, 8 };
        }
        public static IEnumerable<object[]> DataForCalculateDominants()
        {
            yield return new object[] { new List<int>() { 0 }, new List<int>() { 0 } };
            yield return new object[] { new List<int>() { 1 }, new List<int>() { 1 } };
            yield return new object[] { new List<int>() { 6, 5, 144 }, new List<int>() { 5, 6, 144 } };
            yield return new object[] { new List<int>() { 6, 5, 5, 144 }, new List<int>() { 5 } };
            yield return new object[] { new List<int>() { 6, 0, 0, 144 }, new List<int>() { 0 } };
            yield return new object[] { new List<int>() { 86, 7, 2137, 86, 2137, 7, 9, 69, 1, 2, 9, 43, 86, 86, 69, 69, 2137, 86 }, new List<int>() { 86 } };
        }
    }
}