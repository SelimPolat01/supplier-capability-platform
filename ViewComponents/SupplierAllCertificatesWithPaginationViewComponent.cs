using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Services;

namespace SupplierCapabilitiesAndManagementSystem.ViewComponents
{
    public class SupplierAllCertificatesWithPaginationViewComponent : ViewComponent
    {

        private readonly ISupplierCertificateService _certificateService;

        public SupplierAllCertificatesWithPaginationViewComponent(ISupplierCertificateService certificateService)
        {
            _certificateService = certificateService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int userId, int pageNumber = 1, string sortBy = "id_asc", string? searchString = null)
        {
            var allCertificatesWithDetailsGetResponseDTO = await _certificateService.FetchAllCertificatesAsync(userId, pageNumber, pageSize: 10, sortBy, searchString);

            return View(allCertificatesWithDetailsGetResponseDTO);
        }
    }
}
