using SupplierCapabilitiesAndManagementSystem.Models.DTO;

namespace SupplierCapabilitiesAndManagementSystem.Services
{
    public interface ISupplierService
    {
        public Task<FetchAllSuppliersGetResponseDTO> FetchAllSuppliersAsync(string sortBy = "id_asc", int pageNumber = 1, int pageSize = 10, string? searchString = null);

        public Task<FetchSupplierGetResponseDTO> FetchSupplierAsync(int supplierId);
    }
}
