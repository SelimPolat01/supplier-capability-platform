using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Services;

namespace SupplierCapabilitiesAndManagementSystem.ViewComponents
{
    public class AllSuppliersMachinesViewComponent : ViewComponent
    {
        private readonly ISupplierMachineService _supplierMachineService;

        public AllSuppliersMachinesViewComponent(ISupplierMachineService supplierMachineService)
        {
            _supplierMachineService = supplierMachineService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string sortBy = "id_asc", string? searchString = null)
        {
            var fetchAllSupplierMachinesGetResponseDTO = await _supplierMachineService.FetchAllSupplierMachinesAsync(pageNumber: 1, pageSize: 10, sortBy, searchString);

            return View(fetchAllSupplierMachinesGetResponseDTO);
        }
    }
}
