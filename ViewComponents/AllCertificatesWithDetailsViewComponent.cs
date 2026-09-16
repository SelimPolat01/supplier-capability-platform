using Microsoft.AspNetCore.Mvc;
using TedarikciKabiliyetYonetimSistemi.Services;

namespace TedarikciKabiliyetYonetimSistemi.ViewComponents
{
    public class AllCertificatesWithDetailsViewComponent : ViewComponent
    {

        private readonly ICertificateService _certificateService;

        public AllCertificatesWithDetailsViewComponent(ICertificateService certificateService)
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
