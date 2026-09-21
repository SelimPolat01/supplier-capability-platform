using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Services;

namespace SupplierCapabilitiesAndManagementSystem.ViewComponents
{
    public class AllSuppliersMachinesWithPaginationViewComponent : ViewComponent
    {
        private readonly ISupplierMachineService _supplierMachineService;

        public AllSuppliersMachinesWithPaginationViewComponent(ISupplierMachineService supplierMachineService)
        {
            _supplierMachineService = supplierMachineService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int pageNumber = 1, string sortBy = "id_asc", string? searchString = null)
        {
            FetchAllSuppliersMachinesGetResponseDTO fetchAllSuppliersMachinesGetResponseDTO = await _supplierMachineService.FetchAllSupplierMachinesAsync(pageNumber, pageSize: 10, sortBy, searchString);

            return View(fetchAllSuppliersMachinesGetResponseDTO);
        }
    }
}
