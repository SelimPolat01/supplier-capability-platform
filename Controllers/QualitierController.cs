using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;

namespace SupplierCapabilitiesAndManagementSystem.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Qualitier, Admin")]
    public class QualitierController : Controller
    {
        [HttpGet("home")]
        public IActionResult Home([FromQuery] int page = 1, [FromQuery] string supplierSortBy = "id_asc", [FromQuery] string purchaserSortBy = "id_asc", [FromQuery] string machineSortBy = "id_asc", [FromQuery] string certificateSortBy = "id_asc")
        {
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSupplierSort = supplierSortBy;
            ViewBag.CurrentPurchaserSort = purchaserSortBy;
            ViewBag.CurrentMachineSort = machineSortBy;
            ViewBag.CurrentCertificateSort = certificateSortBy;

            return View();
        }

        [HttpGet("all-suppliers")]
        public IActionResult AllSuppliers([FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc", [FromQuery] string? searchString = null)
        {
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentSearch = searchString;

            return View();
        }

        [HttpGet("suppliers/{supplierId:int}")]
        public IActionResult Supplier(int supplierId)
        {
            ViewBag.SupplierId = supplierId;

            return View();
        }

        [HttpGet("all-suppliers-certificates")]
        public IActionResult AllSuppliersCertificates([FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc", string? searchString = null)
        {
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentSearch = searchString;

            return View();
        }

        [HttpGet("suppliers/{supplierId:int}/certificates")]
        public IActionResult SupplierCertificates([FromRoute] int supplierId, [FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc", string? searchString = null)
        {
            ViewBag.SupplierId = supplierId;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentSearch = searchString;

            return View();
        }

        [HttpGet("suppliers/{supplierId:int}/machines")]
        public IActionResult SupplierMachines([FromRoute] int supplierId, [FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc", [FromQuery] string? searchString = null)
        {
            ViewBag.SupplierId = supplierId;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentSearch = searchString;

            return View();
        }

        [HttpGet("suppliers/{supplierId:int}/human-resources")]
        public IActionResult SupplierHumanResources([FromRoute] int supplierId)
        {
            ViewBag.SupplierId = supplierId;

            return View(new FetchSupplierHumanResourcesGetResponseDTO());
        }

        [HttpGet("suppliers/{supplierId:int}/machines/{machineId:int}")]
        public IActionResult SupplierMachine(int machineId, [FromRoute] int supplierId)
        {
            ViewBag.SupplierId = supplierId;
            ViewBag.MachineId = machineId;

            return View();
        }

        [HttpGet("suppliers/{supplierId:int}/certificates/{certificateId:int}")]
        public IActionResult SupplierCertificate(int certificateId, [FromRoute] int supplierId)
        {
            ViewBag.SupplierId = supplierId;
            ViewBag.CertificateId = certificateId;

            return View();
        }

        [HttpGet("all-purchasers")]
        public IActionResult AllPurchasers([FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc", [FromQuery] string? searchString = null)
        {
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentSearch = searchString;

            return View();
        }

        [HttpGet("purchasers/{purchaserId:int}")]
        public IActionResult Purchaser(int purchaserId)
        {
            ViewBag.PurchaserId = purchaserId;

            return View();
        }

        [HttpGet("purchasers/{purchaserId:int}/purchases")]
        public IActionResult PurchaserPurchases([FromRoute] int purchaserId, [FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc", [FromQuery] string? searchString = null)
        {
            ViewBag.PurchaserId = purchaserId;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentSearch = searchString;

            return View();
        }

        [HttpGet("purchasers/{purchaserId:int}/purchases/{purchaseId:int}")]
        public IActionResult PurchaserPurchase([FromRoute] int purchaserId, [FromRoute] int purchaseId)
        {
            ViewBag.PurchaseId = purchaseId;

            return View();
        }

        [HttpGet("all-suppliers-machines")]
        public IActionResult AllSuppliersMachines([FromRoute] int supplierId, [FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc", string? searchString = null)
        {
            ViewBag.SupplierId = supplierId;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentSearch = searchString;

            return View();
        }
    }
}