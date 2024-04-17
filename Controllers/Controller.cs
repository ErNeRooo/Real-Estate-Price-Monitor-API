using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;

namespace repmAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        [HttpGet()] 
        public ActionResult Get()
        {
            return Ok("Pozdro");
        }
    }
}