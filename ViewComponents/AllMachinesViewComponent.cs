using Microsoft.AspNetCore.Mvc;
using TedarikciKabiliyetYonetimSistemi.Services;

namespace TedarikciKabiliyetYonetimSistemi.ViewComponents
{
    public class AllMachinesViewComponent : ViewComponent
    {
        private readonly IMachineService _machineService;

        public AllMachinesViewComponent(IMachineService machineService)
        {
            _machineService = machineService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId, string sortBy = "id_asc")
        {
            var allMachinesGetResponseDTO = await _machineService.FetchAllMachinesAsync(userId, pageNumber: 1, pageSize: 8, sortBy);

            return View(allMachinesGetResponseDTO);
        }
    }
}
