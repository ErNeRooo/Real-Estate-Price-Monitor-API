using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using repmAPI.Services;
using repmAPI.Context;
using RealEstatePriceMonitor.Context;
using RealEstatePriceMonitor.Classes;
using Microsoft.VisualBasic;

namespace repmAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private ScrapingContext scrapingContext;
        private DataService dataService;
        private string[] cities = {
                "Warszawa", "Kraków", "Wrocław", "Łódź", "Poznań", "Gdańsk", "Szczecin",
                "Bydgoszcz", "Lublin", "Białystok", "Katowice", "Toruń", "Rzeszów",
                "Kielce", "Olsztyn", "Gorzów Wielkopolski", "Zielona Góra", "Opole"
            };
        private string NotFoundMessage = "API supports only voivodeship cities from Poland";
        private DbContext dbContext;

        public Controller()
        {
            scrapingContext = new ScrapingContext();
            dataService = new DataService();
            dbContext = new DbContext();
        }

        [HttpGet("getAverage/{cityName}")] 
        public ActionResult<int> GetAverage([FromRoute] string cityName)
        {
            if (cities.Contains(cityName))
            {
                return Ok(
                dataService.CalculateAverage(
                    scrapingContext.GetPrices(cityName)
                    )
                );
            }
            else
            {
                return NotFound(NotFoundMessage);
            }
        }
        
        [HttpGet("getMedian/{cityName}")]
        public ActionResult GetMedian([FromRoute] string cityName)
        {
            if (cities.Contains(cityName))
            {
                return Ok(
                dataService.CalculateMedian(
                    scrapingContext.GetPrices(cityName)
                    )
                );
            }
            else
            {
                return NotFound(NotFoundMessage);
            }
        }
        
        [HttpGet("getDominants/{cityName}")]
        public ActionResult<List<int>> GetDominants([FromRoute] string cityName)
        {
            if (cities.Contains(cityName))
            {
                return Ok(
                dataService.CalculateDominants(
                    scrapingContext.GetPrices(cityName)
                    )
                );
            }
            else
            {
                return NotFound(NotFoundMessage);
            }
        }

        [HttpGet("getPrices/{cityName}")]
        public ActionResult<List<int>> GetPrices([FromRoute] string cityName)
        {

            if (cities.Contains(cityName))
            {
                string id = "2";

                var day = DateTime.Now.Day;
                var month = DateTime.Now.Month;
                var year = DateTime.Now.Year;

                int[] prices = scrapingContext.GetPrices(cityName).ToArray();

                var data = new PriceDataRecord(id, cityName, $"{day}-{month}-{year}", prices);

                dbContext.add(data);
                return Ok(prices);
            }
            else
            {
                return NotFound(NotFoundMessage);
            }
        }
    }
}