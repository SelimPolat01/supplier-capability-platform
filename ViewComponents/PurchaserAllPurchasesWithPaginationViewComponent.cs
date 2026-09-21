using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Services;

namespace SupplierCapabilitiesAndManagementSystem.ViewComponents
{
    public class PurchaserAllPurchasesWithPaginationViewComponent : ViewComponent
    {
        private readonly IPurchaserService _purchaserService;

        public PurchaserAllPurchasesWithPaginationViewComponent(IPurchaserService purchaserService)
        {
            _purchaserService = purchaserService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int purchaserId, string sortBy = "id_asc", int pageNumber = 1, int pageSize = 10, string? searchString = null)
        {
            FetchPurchaserAllPurchasesGetResponseDTO fetchAllPurchasesGetResponseDTO = await _purchaserService.FetchPurchaserAllPurchasesAsync(purchaserId, sortBy, pageNumber, pageSize: 10, searchString);

            return View(fetchAllPurchasesGetResponseDTO);
        }
    }
}
