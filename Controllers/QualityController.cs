using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TedarikciKabiliyetYonetimSistemi.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Quality")]
    public class QualityController : Controller
    {
        [HttpGet("home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
