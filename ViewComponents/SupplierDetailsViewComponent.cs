using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Services;

namespace SupplierCapabilitiesAndManagementSystem.ViewComponents
{
    public class SupplierDetailsViewComponent : ViewComponent
    {
        private readonly ISupplierService _supplierService;

        public SupplierDetailsViewComponent(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int supplierId)
        {
            FetchSupplierGetResponseDTO fetchSupplierGetResponseDTO = await _supplierService.FetchSupplierAsync(supplierId);

            return View(fetchSupplierGetResponseDTO);
        }
    }
}
