using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

