using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Services;

namespace SupplierCapabilitiesAndManagementSystem.ViewComponents
{
    public class SupplierAllCertificatesViewComponent : ViewComponent
    {
        private readonly ISupplierCertificateService _certificateService;

        public SupplierAllCertificatesViewComponent(ISupplierCertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId, string sortBy = "name_asc")
        {
            var allCertificatesGetResponseDTO = await _certificateService.FetchAllCertificatesAsync(userId, pageNumber: 1, pageSize: 10, sortBy);

            return View(allCertificatesGetResponseDTO);
        }
    }
}
