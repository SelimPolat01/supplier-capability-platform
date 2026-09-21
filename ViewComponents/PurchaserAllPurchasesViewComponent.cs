using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Services;

namespace SupplierCapabilitiesAndManagementSystem.ViewComponents
{
    public class PurchaserAllPurchasesViewComponent : ViewComponent
    {
        private readonly IPurchaserService _purchaserService;

        public PurchaserAllPurchasesViewComponent(IPurchaserService purchaserService)
        {
            _purchaserService = purchaserService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int purchaserId, string sortBy = "id_asc", int pageNumber = 1, int pageSize = 10, string? searchString = null)
        {
            FetchPurchaserAllPurchasesGetResponseDTO fetchPurchaserAllPurchasesGetResponseDTO = await _purchaserService.FetchPurchaserAllPurchasesAsync(purchaserId, sortBy, pageNumber, pageSize, searchString);

            return View(fetchPurchaserAllPurchasesGetResponseDTO);
        }
    }
}
