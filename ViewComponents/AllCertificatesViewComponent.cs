using Microsoft.AspNetCore.Mvc;
using TedarikciKabiliyetYonetimSistemi.Services;

namespace TedarikciKabiliyetYonetimSistemi.ViewComponents
{
    public class AllCertificatesViewComponent : ViewComponent
    {
        private readonly ICertificateService _certificateService;

        public AllCertificatesViewComponent(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId, string sortBy = "name_asc")
        {
            var allCertificatesGetResponseDTO = await _certificateService.FetchAllCertificatesAsync(userId, pageNumber: 1, pageSize: 8, sortBy);

            return View(allCertificatesGetResponseDTO);
        }
    }
}
