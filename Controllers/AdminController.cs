using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TedarikciKabiliyetYonetimSistemi.Entities;
using TedarikciKabiliyetYonetimSistemi.Models.DTO;
using TedarikciKabiliyetYonetimSistemi.Services;

namespace TedarikciKabiliyetYonetimSistemi.Controllers
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
        public IActionResult Home([FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc")
        {
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSupplierSort = sortBy;

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
        public IActionResult SupplierMachines([FromRoute] int supplierId, [FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc", string? searchString = null)
        {
            ViewBag.SupplierId = supplierId;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentSearch = searchString;

            return View();
        }

        [HttpGet("all-purchasers")]
        public IActionResult AllPurchasers()
        {
            return View();
        }

        [HttpGet("purchasers/{purchaserId:int}")]
        public IActionResult Purchaser(int purchaserId)
        {
            return View();
        }

        [HttpGet("all-qualitiers")]
        public IActionResult AllQualitiers()
        {
            return View();
        }

        [HttpGet("qualitiers/{qualitierId:int}")]
        public IActionResult Qualitier(int qualitierId)
        {
            return View();
        }

        [HttpGet("suppliers/{supplierId:int}/certificates/{certificateId:int}")]
        public async Task<IActionResult> SupplierCertificate(int certificateId, [FromRoute] int supplierId)
        {
            var certificate = await _supplierCertificateService.FetchCertificateAsync(supplierId, certificateId);

            if (certificate == null) return NotFound("Supplier certificate not found.");

            return View(certificate);
        }

        [HttpGet("suppliers/{supplierId:int}/machines/{machineId:int}")]
        public async Task<IActionResult> SupplierMachine(int machineId, [FromRoute] int supplierId)
        {
            var machine = await _supplierMachineService.FetchMachineAsync(supplierId, machineId);

            if (machine == null) return NotFound("Supplier machine not found.");

            return View(machine);
        }

        [HttpGet("suppliers/{supplierId:int}/human-resources")]
        public IActionResult SupplierHumanResources([FromRoute] int supplierId)
        {
            ViewBag.SupplierId = supplierId;

            return View(new SupplierCertificate());
        }

        [HttpPatch("edit-human-resource/patch")]
        public async Task<IActionResult> EditSupplierHumanResource([FromQuery] int supplierId, [FromBody] EditSupplierHumanResourcePatchRequestDTO editSupplierHumanRespourcePostRequestDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            EditSupplierHumanResourcePatchResponseDTO editSupplierHumanResourcePatchResponseDTO = await _supplierHumanResourceService.EditHumanResourcesAsync(editSupplierHumanRespourcePostRequestDTO, supplierId);

            if (!editSupplierHumanResourcePatchResponseDTO.IsSuccess) return BadRequest(new { message = editSupplierHumanResourcePatchResponseDTO.Message });

            return Ok(new { message = editSupplierHumanResourcePatchResponseDTO.Message });
        }

        [HttpDelete("certificates/{certificateId:int}/delete")]
        public async Task<IActionResult> DeleteSupplierCertificate([FromQuery] int userId, [FromRoute] int certificateId)
        {
            RemoveSupplierCertificateDeleteResponseDTO removeCertificateDeleteResponseDTO = await _supplierCertificateService.RemoveCertificateAsync(userId, certificateId);

            if (removeCertificateDeleteResponseDTO.IsSuccess) return Ok(new { message = removeCertificateDeleteResponseDTO.Message });
            else return BadRequest(new { message = removeCertificateDeleteResponseDTO.Message });
        }

        [HttpDelete("machines/{machineId:int}/delete")]
        public async Task<IActionResult> DeleteSupplierMachine([FromQuery] int userId, [FromRoute] int machineId)
        {
            RemoveSupplierMachineDeleteResponseDTO removeMachineDeleteResponseDTO = await _supplierMachineService.RemoveMachineAsync(userId, machineId);

            if (removeMachineDeleteResponseDTO.IsSuccess) return Ok(new { message = removeMachineDeleteResponseDTO.Message });
            else return BadRequest(new { message = removeMachineDeleteResponseDTO.Message });
        }
    }
}
