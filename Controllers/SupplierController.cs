using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SupplierCapabilitiesAndManagementSystem.Entities;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Services;

namespace SupplierCapabilitiesAndManagementSystem.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Supplier, Purchasing, Quality, Admin")]
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
        public async Task<IActionResult> Machine(int machineId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId)) return RedirectToAction("Login", "Auth");

            var machine = await _supplierMachineService.FetchMachineAsync(userId, machineId);

            if (machine == null) return NotFound("Machine not found.");

            return View(machine);
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
        public async Task<IActionResult> Certificate(int certificateId)
        {
            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId)) return RedirectToAction("Login", "Auth");

            var existingCertificate = await _supplierCertificateService.FetchCertificateAsync(userId, certificateId);

            if (existingCertificate == null) return NotFound("Certificate not found.");

            return View(existingCertificate);
        }


        [HttpGet("manage-human-resource")]
        public IActionResult ManageHumanResource()
        {
            return View(new SupplierHumanResource());
        }

        [HttpPatch("machines/{machineId:int}/patch")]
        public async Task<IActionResult> PatchMachine([FromRoute] int machineId, [FromBody] EditSupplierMachinePatchRequestDTO editMachinePatchRequestDTO)
        {
            if (machineId != editMachinePatchRequestDTO.Id) return BadRequest("ID mismatch");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId)) return Unauthorized(new { message = "Please log in again." });

            var machineUpdatingResult = await _supplierMachineService.EditMachineAsync(editMachinePatchRequestDTO, userId);

            if (!machineUpdatingResult.IsSuccess) return Unauthorized(machineUpdatingResult.Message);

            return Ok(new { message = machineUpdatingResult.Message });
        }

        [HttpPatch("certificates/{certificateId:int}/patch")]
        public async Task<IActionResult> PatchCertificate(int certificateId, [FromBody] EditSupplierCertificatePatchRequestDTO editCertificatePatchRequestDTO)
        {
            if (editCertificatePatchRequestDTO.Id != certificateId) return BadRequest("ID mismatch");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId)) return Unauthorized(new { message = "Please log in again." });

            var certificateUpdatingResult = await _supplierCertificateService.EditCertificateAsync(editCertificatePatchRequestDTO, userId);

            if (!certificateUpdatingResult.IsSuccess) return BadRequest(certificateUpdatingResult.Message);

            return Ok(new { message = certificateUpdatingResult.Message });
        }

        [HttpPatch("edit-human-resource/patch")]
        public async Task<IActionResult> EditHumanResource([FromBody] EditSupplierHumanResourcePatchRequestDTO editSupplierHumanRespourcePostRequestDTO)
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
