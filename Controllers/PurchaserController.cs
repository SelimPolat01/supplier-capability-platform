using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Services;
using System.Security.Claims;

namespace SupplierCapabilitiesAndManagementSystem.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Purchaser, Admin")]
    public class PurchaserController : Controller
    {
        private readonly ISupplierCertificateService _supplierCertificateService;
        private readonly ISupplierMachineService _supplierMachineService;
        private readonly IPurchaserService _purchaserService;

        public PurchaserController(ISupplierCertificateService supplierCertificateService, ISupplierMachineService supplierMachineService, IPurchaserService purchaserService)
        {
            _supplierCertificateService = supplierCertificateService;
            _supplierMachineService = supplierMachineService;
            _purchaserService = purchaserService;
        }

        [HttpGet("home")]
        public IActionResult Home([FromQuery] string supplierSortBy = "id_asc", [FromQuery] string machineSortBy = "id_asc", [FromQuery] string purchaseSortBy = "id_asc")
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userIdString, out var userId);

            ViewBag.PurchaserId = userId;
            ViewBag.CurrentPurchaseSort = purchaseSortBy;
            ViewBag.CurrentSupplierSort = supplierSortBy;
            ViewBag.CurrentMachineSort = machineSortBy;

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

        [HttpGet("suppliers/{supplierId:int}/certificates")]
        public IActionResult SupplierCertificates([FromRoute] int supplierId, [FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc", string? searchString = null)
        {
            ViewBag.SupplierId = supplierId;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentSearch = searchString;

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

        [HttpGet("suppliers/{supplierId:int}/machines")]
        public IActionResult SupplierMachines([FromRoute] int supplierId, [FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc", string? searchString = null)
        {
            ViewBag.SupplierId = supplierId;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentSearch = searchString;

            return View();
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

        [HttpGet("suppliers/{supplierId:int}/human-resources")]
        public IActionResult SupplierHumanResources([FromRoute] int supplierId)
        {
            ViewBag.SupplierId = supplierId;

            return View(new FetchSupplierHumanResourcesGetResponseDTO());
        }

        [HttpGet("purchases")]
        public IActionResult Purchases([FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc", [FromQuery] string? searchString = null)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userIdString, out var userId);

            ViewBag.PurchaserId = userId;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentSearch = searchString;

            return View();
        }

        [HttpGet("purchases/{purchaseId}")]
        public IActionResult Purchase([FromRoute] int purchaseId)
        {
            ViewBag.PurchaseId = purchaseId;

            return View();
        }

        [HttpPost("purchases/add/{machineId:int}")]
        public async Task<IActionResult> PurchaseCreate([FromRoute] int machineId, [FromQuery] int quantity)
        {
            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out int purchaserId)) return Unauthorized(new { message = "Please log in again." });

            AddPurchaserMachinePurchasePostResponseDTO result = await _purchaserService.AddPurchaserMachinePurchaseAsync(purchaserId, machineId, quantity);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Message });

            return Ok(new
            {
                message = result.Message,
                addedMachinePurchaseId = result.AddedMachinePurchaseId
            });
        }
    }
}
