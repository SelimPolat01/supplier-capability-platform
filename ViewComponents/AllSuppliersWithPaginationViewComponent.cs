using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Services;

namespace SupplierCapabilitiesAndManagementSystem.ViewComponents
{
    public class AllSuppliersWithPaginationViewComponent : ViewComponent
    {
        private readonly ISupplierService _supplierService;

        public AllSuppliersWithPaginationViewComponent(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int pageNumber = 1, string sortBy = "id_asc", string? searchString = null)
        {
            FetchAllSuppliersGetResponseDTO fetchAllSuppliersGetResponseDTO = await _supplierService.FetchAllSuppliersAsync(sortBy, pageNumber, pageSize: 10, searchString);

            return View(fetchAllSuppliersGetResponseDTO);
        }
    }
}
