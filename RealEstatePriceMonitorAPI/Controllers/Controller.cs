using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using repmAPI.Services;
using repmAPI.Context;

namespace repmAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private ScrapingContext scrapingContext;
        private DataService dataService;
        public Controller()
        {
            scrapingContext = new ScrapingContext();
            dataService = new DataService();
        }

        [HttpGet("getAverage")] 
        public ActionResult GetAverage()
        {
            return Ok(
                dataService.CalculateAverage(
                    scrapingContext.GetPrices()
                    )
                );
        }
        
        [HttpGet("getMedian")]
        public ActionResult GetMedian()
        {
            return Ok(
                dataService.CalculateMedian(
                    scrapingContext.GetPrices()
                    )
                );
        }
        
        [HttpGet("getDominants")]
        public ActionResult GetDominants()
        {
            return Ok(
                dataService.CalculateDominants(
                    scrapingContext.GetPrices()
                    )
                );
        }

        [HttpGet("getPrices")]
        public ActionResult GetPrices()
        {
            return Ok(scrapingContext.GetPrices());
        }
    }
}