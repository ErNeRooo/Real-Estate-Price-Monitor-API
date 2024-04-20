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

        [HttpGet()] 
        public ActionResult GetAverage()
        {
            return Ok(
                dataService.GetDominants(
                    scrapingContext.GetPrices()
                    )
                );
        }
    }
}