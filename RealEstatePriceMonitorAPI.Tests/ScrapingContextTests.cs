using repmAPI.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePriceMonitorAPI.Tests
{
    public class ScrapingContextTests
    {
        [Theory]
        [InlineData("dsh9d9 2dhsd0 3374e23", "9920337423")]
        [InlineData("Q!w@2e#R$t % Y^u&i*opasdg1(){3};:7d|", "2137")]
        [InlineData("12 345@67 8#90 PO$ZDRO A aĄą  B b%   0CcĆć^^D dEeĘęF&fGg 9Hh8Ii J jKkL*7l(Łł# 6MmN5nOoÓóPpQ4q R # rSsŚś TtU3uVvWwX xY2yZzŹź1ŻżZz", "12345678900987654321")]
        [InlineData("", "")]
        [InlineData("KokCDidj  FejEIFDLefpKEOFaefOPopefajfleFjefowpuencuvioo", "")]
        [InlineData(" 1 23 4   5 ", "12345")]
        public void removeAllCharsFromStringExceptNumbers_ForGivenString_ReturnsOnlyNumbersOfThatString(string value, string expected)
        {
            // assign
            ScrapingContext scrapingContext = new ScrapingContext();

            // act
            var result = scrapingContext.removeAllCharsFromStringExceptNumbers(value);

            // assert
            Assert.Equal(expected, result);
        }
    }
}
