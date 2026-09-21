using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Services;

namespace SupplierCapabilitiesAndManagementSystem.ViewComponents
{
    public class AllQualitiersViewComponent : ViewComponent
    {
        private readonly IQualitierService _qualitierService;

        public AllQualitiersViewComponent(IQualitierService qualitierService)
        {
            _qualitierService = qualitierService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int pageNumber = 1, string sortBy = "id_asc", string? searchString = null)
        {
            FetchAllQualitiersGetResponseDTO fetchAllQualitiersGetResponseDTO = await _qualitierService.FetchAllQualitiersAsync(sortBy, pageNumber, pageSize: 10, searchString);

            return View(fetchAllQualitiersGetResponseDTO);
        }
    }
}
