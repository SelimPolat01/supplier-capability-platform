using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Services;

namespace SupplierCapabilitiesAndManagementSystem.ViewComponents
{
    public class AllQualitiersWithPaginationViewComponent : ViewComponent
    {
        private readonly IQualitierService _qualitierService;

        public AllQualitiersWithPaginationViewComponent(IQualitierService qualitierService)
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
