using Microsoft.AspNetCore.Mvc;

namespace URLShortener.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        // GET api/home
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("URL Shortener API is running ✅");
        }
    }
}
