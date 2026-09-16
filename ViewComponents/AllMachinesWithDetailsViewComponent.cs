using Microsoft.AspNetCore.Mvc;
using TedarikciKabiliyetYonetimSistemi.Services;

namespace TedarikciKabiliyetYonetimSistemi.ViewComponents
{
    public class AllMachinesWithDetailsViewComponent : ViewComponent
    {
        private readonly IMachineService _machineService;

        public AllMachinesWithDetailsViewComponent(IMachineService machineService)
        {
            _machineService = machineService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId, int pageNumber = 1, string sortBy = "id_asc", string? searchString = null)
        {
            var allMachinesWithDetailsGetResponseDTO = await _machineService.FetchAllMachinesAsync(userId, pageNumber, pageSize: 10, sortBy, searchString);

            return View(allMachinesWithDetailsGetResponseDTO);
        }
    }
}
