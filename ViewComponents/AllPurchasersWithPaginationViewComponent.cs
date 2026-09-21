using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Services;

namespace SupplierCapabilitiesAndManagementSystem.ViewComponents
{
    public class AllPurchasersWithPaginationViewComponent : ViewComponent
    {
        private readonly IPurchaserService _purchaserService;

        public AllPurchasersWithPaginationViewComponent(IPurchaserService purchaserService)
        {
            _purchaserService = purchaserService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int pageNumber = 1, string sortBy = "id_asc", string? searchString = null)
        {
            FetchAllPurchasersGetResponseDTO fetchAllPurchasersGetResponseDTO = await _purchaserService.FetchAllPurchasersAsync(sortBy, pageNumber, pageSize: 10, searchString);

            return View(fetchAllPurchasersGetResponseDTO);
        }
    }
}
