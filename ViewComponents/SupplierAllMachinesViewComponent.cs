using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Services;

namespace SupplierCapabilitiesAndManagementSystem.ViewComponents
{
    public class SupplierAllMachinesViewComponent : ViewComponent
    {
        private readonly ISupplierMachineService _machineService;

        public SupplierAllMachinesViewComponent(ISupplierMachineService machineService)
        {
            _machineService = machineService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId, string sortBy = "id_asc")
        {
            var allMachinesGetResponseDTO = await _machineService.FetchAllMachinesAsync(userId, pageNumber: 1, pageSize: 10, sortBy);

            return View(allMachinesGetResponseDTO);
        }
    }
}
