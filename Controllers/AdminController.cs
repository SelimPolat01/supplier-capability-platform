using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TedarikciKabiliyetYonetimSistemi.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        [HttpGet("home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
