using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Enums;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Services;
using System.Security.Claims;

namespace SupplierCapabilitiesAndManagementSystem.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Qualitier, Admin")]
    public class QualitierController : Controller
    {
        private readonly IQualitierService _qualitierService;
        public QualitierController(IQualitierService qualitierService)
        {
            _qualitierService = qualitierService;
        }

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

            return View(new FetchSupplierHumanResourceGetResponseDTO());
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

        [HttpPatch("suppliers/{supplierId:int}/machines/{machineId:int}")]
        public async Task<IActionResult> ScoreMachineAsync([FromRoute] int supplierId, [FromRoute] int machineId, [FromQuery] QualityScore? qualityScore)
        {
            if (qualityScore == null) return BadRequest(new { message = "Please select a score." });

            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out var userId) || userId <= 0) return Unauthorized(new { message = "User is not authorized or session expired." });

            EditSupplierMachineQualityPatchResponseDTO responseDTO = await _qualitierService.EditSupplierMachineQualityAsync(userId, machineId, qualityScore);

            if (!responseDTO.IsSuccess) return BadRequest(new { message = responseDTO.Message });

            return Ok(new { message = responseDTO.Message });
        }

        [HttpPatch("suppliers/{supplierId:int}/certificates/{certificateId:int}")]
        public async Task<IActionResult> ScoreCertificateAsync([FromRoute] int supplierId, [FromRoute] int certificateId, [FromQuery] QualityScore? qualityScore)
        {
            if (qualityScore == null) return BadRequest(new { message = "Please select a score." });

            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out var userId) || userId <= 0) return Unauthorized(new { message = "User is not authorized or session expired." });

            EditSupplierCertificateQualityPatchResponseDTO responseDTO = await _qualitierService.EditSupplierCertificateQualityAsync(userId, certificateId, qualityScore);

            if (!responseDTO.IsSuccess) return BadRequest(new { message = responseDTO.Message });

            return Ok(new { message = responseDTO.Message });
        }

        [HttpPatch("suppliers/{supplierId:int}/human-resources/{humanResource:int}")]
        public async Task<IActionResult> ScoreHumanResourceAsync([FromRoute] int supplierId, [FromRoute] int humanResourceId, [FromQuery] QualityScore? qualityScore)
        {
            if (qualityScore == null) return BadRequest(new { message = "Please select a score." });

            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out var userId) || userId <= 0) return Unauthorized(new { message = "User is not authorized or session expired." });

            EditSupplierHumanResourceQualityPatchResponseDTO responseDTO = await _qualitierService.EditSupplierHumanResourceQualityAsync(userId, humanResourceId, qualityScore);

            if (!responseDTO.IsSuccess) return BadRequest(new { message = responseDTO.Message });

            return Ok(new { message = responseDTO.Message });
        }
    }
}