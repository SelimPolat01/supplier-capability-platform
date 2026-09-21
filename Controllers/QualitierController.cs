using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SupplierCapabilitiesAndManagementSystem.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Qualitier, Admin")]
    public class QualitierController : Controller
    {
        [HttpGet("home")]
        public IActionResult Home([FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc")
        {
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSupplierSort = sortBy;

            return View();
        }
    }
}
