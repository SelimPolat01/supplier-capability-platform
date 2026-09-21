using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Services;

namespace SupplierCapabilitiesAndManagementSystem.ViewComponents
{
    public class SupplierAllMachinesWithPaginationViewComponent : ViewComponent
    {
        private readonly ISupplierMachineService _machineService;

        public SupplierAllMachinesWithPaginationViewComponent(ISupplierMachineService machineService)
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
