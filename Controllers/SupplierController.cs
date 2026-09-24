using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Entities;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Services;
using System.Security.Claims;

namespace SupplierCapabilitiesAndManagementSystem.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Supplier, Purchaser, Qualitier, Admin")]
    public class SupplierController : Controller
    {
        private readonly ISupplierMachineService _supplierMachineService;
        private readonly ISupplierCertificateService _supplierCertificateService;
        private readonly ISupplierHumanResourceService _supplierHumanResourceService;
        public SupplierController(ISupplierMachineService machineService, ISupplierCertificateService certificateService, ISupplierHumanResourceService supplierHumanResourceService)
        {
            _supplierMachineService = machineService;
            _supplierCertificateService = certificateService;
            _supplierHumanResourceService = supplierHumanResourceService;
        }

        [HttpGet("home")]
        public IActionResult Home([FromQuery] string machineSortBy = "group_asc", [FromQuery] string certificateSortBy = "name_asc")
        {
            ViewBag.CurrentMachineSort = machineSortBy;
            ViewBag.CurrentCertificateSort = certificateSortBy;

            return View();
        }

        [HttpGet("add-machine")]
        public IActionResult AddMachine()
        {
            return View(new AddSupplierMachinePostRequestDTO());
        }

        [HttpPost("add-machine")]
        public async Task<IActionResult> AddMachine(AddSupplierMachinePostRequestDTO addSupplierMachinePostRequestDTO)
        {
            if (!ModelState.IsValid) return View(addSupplierMachinePostRequestDTO);

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId)) return RedirectToAction("Login", "Auth");

            AddSupplierMachinePostResponseDTO addMachinePostResponseDTO = await _supplierMachineService.AddMachineAsync(userId, addSupplierMachinePostRequestDTO);

            if (!addMachinePostResponseDTO.IsSuccess)
            {
                ModelState.AddModelError("", addMachinePostResponseDTO.Message);

                return View(addSupplierMachinePostRequestDTO);
            }
            else return RedirectToAction("Machines", "Supplier");
        }

        [HttpGet("add-certificate")]
        public IActionResult AddCertificate()
        {
            return View(new AddSupplierCertificatePostRequestDTO());
        }

        [HttpPost("add-certificate")]
        public async Task<IActionResult> AddCertificate(AddSupplierCertificatePostRequestDTO addCertificatePostRequestDTO)
        {
            if (!ModelState.IsValid) return View(addCertificatePostRequestDTO);

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId)) return RedirectToAction("Login", "Auth");

            AddSupplierCertificatePostResponseDTO addCertificatePostResponseDTO = await _supplierCertificateService.AddCertificateAsync(userId, addCertificatePostRequestDTO);

            if (!addCertificatePostResponseDTO.IsSuccess)
            {
                ModelState.AddModelError("", addCertificatePostResponseDTO.Message);

                return View(addCertificatePostRequestDTO);
            }
            else return RedirectToAction("Certificates", "Supplier");
        }

        [HttpGet("machines")]
        public IActionResult Machines([FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc", [FromQuery] string? searchString = null)
        {
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentSearch = searchString;

            return View();
        }

        [HttpGet("machines/{machineId:int}")]
        public IActionResult Machine(int machineId)
        {
            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userIdString, out var userId);

            ViewBag.UserId = userId;
            ViewBag.MachineId = machineId;

            return View();
        }

        [HttpGet("certificates")]
        public IActionResult Certificates([FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc", [FromQuery] string? searchString = null)
        {
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentSearch = searchString;

            return View();
        }

        [HttpGet("certificates/{certificateId:int}")]
        public IActionResult Certificate(int certificateId)
        {
            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userIdString, out var userId);

            ViewBag.UserId = userId;
            ViewBag.CertificateId = certificateId;

            return View();
        }

        [HttpGet("manage-human-resource")]
        public IActionResult ManageHumanResource()
        {
            return View(new SupplierHumanResource());
        }

        [HttpPatch("machines/{machineId:int}/patch")]
        public async Task<IActionResult> PatchMachine([FromRoute] int machineId, [FromBody] EditSupplierMachinePatchRequestDTO editMachinePatchRequestDTO)
        {
            if (editMachinePatchRequestDTO == null) return BadRequest(new { message = "The submitted data format is invalid." });
            if (machineId != editMachinePatchRequestDTO.Id) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId)) return Unauthorized(new { message = "Please log in again." });

            var machineUpdatingResult = await _supplierMachineService.EditMachineAsync(editMachinePatchRequestDTO, userId);

            if (!machineUpdatingResult.IsSuccess) return BadRequest(new { message = machineUpdatingResult.Message });

            return Ok(new { message = machineUpdatingResult.Message });
        }

        [HttpPatch("certificates/{certificateId:int}/patch")]
        public async Task<IActionResult> PatchCertificate(int certificateId, [FromBody] EditSupplierCertificatePatchRequestDTO editCertificatePatchRequestDTO)
        {
            if (editCertificatePatchRequestDTO == null) return BadRequest(new { message = "The submitted data format is invalid." });
            if (editCertificatePatchRequestDTO.Id != certificateId) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId)) return Unauthorized(new { message = "Please log in again." });

            var certificateUpdatingResult = await _supplierCertificateService.EditCertificateAsync(editCertificatePatchRequestDTO, userId);

            if (!certificateUpdatingResult.IsSuccess) return BadRequest(new { message = certificateUpdatingResult.Message });

            return Ok(new { message = certificateUpdatingResult.Message });
        }

        [HttpPatch("edit-human-resource/patch")]
        public async Task<IActionResult> PatchHumanResource([FromBody] EditSupplierHumanResourcePatchRequestDTO editSupplierHumanRespourcePostRequestDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out int userId)) return Unauthorized("Please log in again.");

            EditSupplierHumanResourcePatchResponseDTO editSupplierHumanResourcePatchResponseDTO = await _supplierHumanResourceService.EditHumanResourcesAsync(editSupplierHumanRespourcePostRequestDTO, userId);

            if (!editSupplierHumanResourcePatchResponseDTO.IsSuccess) return BadRequest(new { message = editSupplierHumanResourcePatchResponseDTO.Message });

            return Ok(new { message = editSupplierHumanResourcePatchResponseDTO.Message });
        }

        [HttpDelete("machines/{machineId:int}/delete")]
        public async Task<IActionResult> DeleteSupplierMachine(int machineId)
        {
            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId)) return Unauthorized(new { message = "Please log in again." });

            RemoveSupplierMachineDeleteResponseDTO removeMachineDeleteResponseDTO = await _supplierMachineService.RemoveMachineAsync(userId, machineId);

            if (removeMachineDeleteResponseDTO.IsSuccess) return Ok(new { message = removeMachineDeleteResponseDTO.Message });
            else return BadRequest(new { message = removeMachineDeleteResponseDTO.Message });
        }

        [HttpDelete("certificates/{certificateId:int}/delete")]
        public async Task<IActionResult> DeleteSupplierCertificate(int certificateId)
        {
            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId)) return Unauthorized(new { message = "Please log in again." });

            RemoveSupplierCertificateDeleteResponseDTO removeCertificateDeleteResponseDTO = await _supplierCertificateService.RemoveCertificateAsync(userId, certificateId);

            if (removeCertificateDeleteResponseDTO.IsSuccess) return Ok(new { message = removeCertificateDeleteResponseDTO.Message });
            else return BadRequest(new { message = removeCertificateDeleteResponseDTO.Message });
        }
    }
}
