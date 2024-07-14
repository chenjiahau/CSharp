using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        // GET: /<controller>/
        [HttpGet]
        [Route("Home")]
        public IActionResult Index()
        {
            return Ok("Home");
        }
    }
}

