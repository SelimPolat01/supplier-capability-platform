using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Services;

namespace SupplierCapabilitiesAndManagementSystem.ViewComponents
{
    public class SupplierHumanResourcesViewComponent : ViewComponent
    {
        private readonly ISupplierHumanResourceService _supplierHumanResourceService;

        public SupplierHumanResourcesViewComponent(ISupplierHumanResourceService supplierHumanResourceService)
        {
            _supplierHumanResourceService = supplierHumanResourceService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            FetchSupplierHumanResourcesGetResponseDTO supplierHumanResourcesGetResponseDTO = await _supplierHumanResourceService.FetchSupplierHumanResourcesAsync(userId);

            return View(supplierHumanResourcesGetResponseDTO.Data);
        }
    }
}
