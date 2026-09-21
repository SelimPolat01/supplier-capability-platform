using SupplierCapabilitiesAndManagementSystem.Models.DTO;

namespace SupplierCapabilitiesAndManagementSystem.Services
{
    public interface IQualitierService
    {
        public Task<FetchAllQualitiersGetResponseDTO> FetchAllQualitiersAsync(string sortBy = "id_asc", int pageNumber = 1, int pageSize = 10, string? searchString = null);
    }
}
