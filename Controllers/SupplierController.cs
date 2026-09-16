using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TedarikciKabiliyetYonetimSistemi.Models.DTO;
using TedarikciKabiliyetYonetimSistemi.Services;

namespace TedarikciKabiliyetYonetimSistemi.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Supplier")]
    public class SupplierController : Controller
    {
        private readonly IMachineService _machineService;
        private readonly ICertificateService _certificateService;
        public SupplierController(IMachineService machineService, ICertificateService certificateService)
        {
            _machineService = machineService;
            _certificateService = certificateService;
        }

        [HttpGet("home")]
        public IActionResult Index([FromQuery] string machineSortBy = "group_asc", [FromQuery] string certificateSortBy = "name_asc")
        {
            ViewBag.CurrentMachineSort = machineSortBy;
            ViewBag.CurrentCertificateSort = certificateSortBy;

            return View();
        }

        [HttpGet("add-machine")]
        public IActionResult AddMachine()
        {
            return View(new AddMachinePostRequestDTO());
        }

        [HttpPost("add-machine")]
        public async Task<IActionResult> AddMachine(AddMachinePostRequestDTO addMachinePostRequestDTO)
        {
            if (!ModelState.IsValid) return View(addMachinePostRequestDTO);

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId)) return RedirectToAction("Login", "Auth");

            AddMachinePostResponseDTO addMachinePostResponseDTO = await _machineService.AddMachineAsync(userId, addMachinePostRequestDTO);

            if (!addMachinePostResponseDTO.IsSuccess)
            {
                ModelState.AddModelError("", addMachinePostResponseDTO.Message);

                return View(addMachinePostRequestDTO);
            }
            else return RedirectToAction("Machines", "Supplier");
        }

        [HttpGet("add-certificate")]
        public IActionResult AddCertificate()
        {
            return View(new AddCertificatePostRequestDTO());
        }

        [HttpPost("add-certificate")]
        public async Task<IActionResult> AddCertificate(AddCertificatePostRequestDTO addCertificatePostRequestDTO)
        {
            if (!ModelState.IsValid) return View(addCertificatePostRequestDTO);

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId)) return RedirectToAction("Login", "Auth");

            AddCertificatePostResponseDTO addCertificatePostResponseDTO = await _certificateService.AddCertificateAsync(userId, addCertificatePostRequestDTO);

            if (!addCertificatePostResponseDTO.IsSuccess)
            {
                ModelState.AddModelError("", addCertificatePostResponseDTO.Message);

                return View(addCertificatePostRequestDTO);
            }
            else return RedirectToAction("Certificates", "Supplier");
        }

        [HttpGet("machines")]
        public ActionResult Machines([FromQuery] int page = 1, [FromQuery] string sortBy = "id_asc", [FromQuery] string? searchString = null)
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

            var machine = await _machineService.FetchMachineAsync(userId, machineId);

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

            var existingCertificate = await _certificateService.FetchCertificateAsync(userId, certificateId);

            if (existingCertificate == null) return NotFound("Certificate not found.");

            return View(existingCertificate);
        }

        [HttpPatch("machines/{machineId:int}/patch")]
        public async Task<IActionResult> PatchMachine([FromRoute] int machineId, [FromBody] EditMachinePatchRequestDTO editMachinePatchRequestDTO)
        {
            if (machineId != editMachinePatchRequestDTO.Id) return BadRequest("ID mismatch");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId)) return Unauthorized(new { message = "Please log in again." });

            var machineUpdatingResult = await _machineService.EditMachineAsync(editMachinePatchRequestDTO, userId);

            if (!machineUpdatingResult.IsSuccess) return Unauthorized(machineUpdatingResult.Message);

            return Ok(new { message = machineUpdatingResult.Message });
        }

        [HttpPatch("certificates/{certificateId:int}/patch")]
        public async Task<IActionResult> PatchCertificate(int certificateId, [FromBody] EditCertificatePatchRequestDTO editCertificatePatchRequestDTO)
        {
            if (editCertificatePatchRequestDTO.Id != certificateId) return BadRequest("ID mismatch");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId)) return Unauthorized(new { message = "Please log in again." });

            var certificateUpdatingResult = await _certificateService.EditCertificateAsync(editCertificatePatchRequestDTO, userId);

            if (!certificateUpdatingResult.IsSuccess) return Unauthorized(certificateUpdatingResult.Message);

            return Ok(new { message = certificateUpdatingResult.Message });
        }

        [HttpDelete("machines/{machineId:int}/delete")]
        public async Task<IActionResult> DeleteMachine(int machineId)
        {
            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId)) return Unauthorized(new { message = "Please log in again." });

            RemoveMachineDeleteResponseDTO removeMachineDeleteResponseDTO = await _machineService.RemoveMachineAsync(userId, machineId);

            if (removeMachineDeleteResponseDTO.IsSuccess) return Ok(new { message = removeMachineDeleteResponseDTO.Message });
            else return BadRequest(new { message = removeMachineDeleteResponseDTO.Message });
        }

        [HttpDelete("certificates/{certificateId:int}/delete")]
        public async Task<IActionResult> DeleteCertificate(int certificateId)
        {
            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId)) return Unauthorized(new { message = "Please log in again." });

            RemoveCertificateDeleteResponseDTO removeCertificateDeleteResponseDTO = await _certificateService.RemoveCertificateAsync(userId, certificateId);

            if (removeCertificateDeleteResponseDTO.IsSuccess) return Ok(new { message = removeCertificateDeleteResponseDTO.Message });
            else return BadRequest(new { message = removeCertificateDeleteResponseDTO.Message });
        }
    }
}
