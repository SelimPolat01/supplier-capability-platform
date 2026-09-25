using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Services;
using System.Security.Claims;

namespace SupplierCapabilitiesAndManagementSystem.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ISupplierCertificateService _supplierCertificateService;
        private readonly ISupplierMachineService _supplierMachineService;
        private readonly ISupplierHumanResourceService _supplierHumanResourceService;


        public AdminController(ISupplierCertificateService supplierCertificateService, ISupplierMachineService supplierMachineService, ISupplierHumanResourceService supplierHumanResourceService)
        {
            _supplierCertificateService = supplierCertificateService;
            _supplierMachineService = supplierMachineService;
            _supplierHumanResourceService = supplierHumanResourceService;
        }

        [HttpGet("home")]
        public IActionResult Home([FromQuery] int page = 1, [FromQuery] string supplierSortBy = "id_asc", [FromQuery] string purchaserSortBy = "id_asc", [FromQuery] string qualitierSortBy = "id_asc", [FromQuery] string machineSortBy = "id_asc", [FromQuery] string certificateSortBy = "id_asc")
        {
            ViewBag.CurrentSupplierSort = supplierSortBy;
            ViewBag.CurrentQualitierSort = qualitierSortBy;
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

        [HttpGet("suppliers/{supplierId:int}/certificates")]
        public IActionResult SupplierCertificates([FromRoute] int supplierId, [FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc", string? searchString = null)
        {
            ViewBag.SupplierId = supplierId;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentSearch = searchString;

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
        public IActionResult SupplierMachines([FromRoute] int supplierId, [FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc", [FromQuery] string? searchString = null)
        {
            ViewBag.SupplierId = supplierId;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentSearch = searchString;

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

        [HttpGet("all-qualitiers")]
        public IActionResult AllQualitiers([FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc", [FromQuery] string? searchString = null)
        {
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentSearch = searchString;

            return View();
        }

        [HttpGet("qualitiers/{qualitierId:int}")]
        public IActionResult Qualitier(int qualitierId)
        {
            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userIdString, out int userId);

            ViewBag.UserId = userId;
            ViewBag.QualitierId = qualitierId;

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

        [HttpGet("suppliers/{supplierId:int}/human-resource")]
        public IActionResult SupplierHumanResource([FromRoute] int supplierId)
        {
            ViewBag.SupplierId = supplierId;

            return View(new FetchSupplierHumanResourceGetResponseDTO());
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

        [HttpPatch("suppliers/{supplierId:int}/machines/{machineId:int}/patch")]
        public async Task<IActionResult> PatchMachine([FromRoute] int supplierId, [FromRoute] int machineId, [FromBody] EditSupplierMachinePatchRequestDTO editMachinePatchRequestDTO)
        {
            if (editMachinePatchRequestDTO == null) return BadRequest(new { message = "The submitted data format is invalid." });
            if (machineId != editMachinePatchRequestDTO.Id) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var machineUpdatingResult = await _supplierMachineService.EditMachineAsync(editMachinePatchRequestDTO, supplierId);

            if (!machineUpdatingResult.IsSuccess) return BadRequest(machineUpdatingResult.Message);

            return Ok(new { message = machineUpdatingResult.Message });
        }

        [HttpPatch("suppliers/{supplierId:int}/certificates/{certificateId:int}/patch")]
        public async Task<IActionResult> PatchCertificate([FromRoute] int supplierId, [FromRoute] int certificateId, [FromBody] EditSupplierCertificatePatchRequestDTO editCertificatePatchRequestDTO)
        {
            if (editCertificatePatchRequestDTO == null) return BadRequest(new { message = "The submitted data format is invalid." });
            if (editCertificatePatchRequestDTO.Id != certificateId) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var certificateUpdatingResult = await _supplierCertificateService.EditCertificateAsync(editCertificatePatchRequestDTO, supplierId);

            if (!certificateUpdatingResult.IsSuccess) return BadRequest(new { message = certificateUpdatingResult.Message });

            return Ok(new { message = certificateUpdatingResult.Message });
        }

        [HttpPatch("edit-human-resource/patch")]
        public async Task<IActionResult> PatchSupplierHumanResource([FromQuery] int supplierId, [FromBody] EditSupplierHumanResourcePatchRequestDTO editSupplierHumanRespourcePostRequestDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            EditSupplierHumanResourcePatchResponseDTO editSupplierHumanResourcePatchResponseDTO = await _supplierHumanResourceService.EditHumanResourcesAsync(editSupplierHumanRespourcePostRequestDTO, supplierId);

            if (!editSupplierHumanResourcePatchResponseDTO.IsSuccess) return BadRequest(new { message = editSupplierHumanResourcePatchResponseDTO.Message });

            return Ok(new { message = editSupplierHumanResourcePatchResponseDTO.Message });
        }

        [HttpDelete("suppliers/{supplierId:int}/machines/{machineId:int}/delete")]
        public async Task<IActionResult> DeleteSupplierMachine([FromRoute] int supplierId, [FromRoute] int machineId)
        {
            RemoveSupplierMachineDeleteResponseDTO removeMachineDeleteResponseDTO = await _supplierMachineService.RemoveMachineAsync(supplierId, machineId);

            if (removeMachineDeleteResponseDTO.IsSuccess) return Ok(new { message = removeMachineDeleteResponseDTO.Message });
            else return BadRequest(new { message = removeMachineDeleteResponseDTO.Message });
        }

        [HttpDelete("suppliers/{supplierId:int}/certificates/{certificateId:int}/delete")]
        public async Task<IActionResult> DeleteSupplierCertificate([FromRoute] int supplierId, [FromRoute] int certificateId)
        {
            RemoveSupplierCertificateDeleteResponseDTO removeCertificateDeleteResponseDTO = await _supplierCertificateService.RemoveCertificateAsync(supplierId, certificateId);

            if (removeCertificateDeleteResponseDTO.IsSuccess) return Ok(new { message = removeCertificateDeleteResponseDTO.Message });
            else return BadRequest(new { message = removeCertificateDeleteResponseDTO.Message });
        }
    }
}
