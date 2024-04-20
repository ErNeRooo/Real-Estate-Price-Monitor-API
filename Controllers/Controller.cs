using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using repmAPI.Services;

namespace repmAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private ScrapingService scrapingService;
        public Controller()
        {
            scrapingService = new ScrapingService();
        }

        [HttpGet()] 
        public ActionResult GetAverage()
        {
            return Ok(scrapingService.GetDominants());
        }
    }
}