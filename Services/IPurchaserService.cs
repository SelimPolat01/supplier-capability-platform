using SupplierCapabilitiesAndManagementSystem.Models.DTO;

namespace SupplierCapabilitiesAndManagementSystem.Services
{
    public interface IPurchaserService
    {
        public Task<FetchAllPurchasersGetResponseDTO> FetchAllPurchasersAsync(string sortBy = "id_asc", int pageNumber = 1, int pageSize = 10, string? searchString = null);

        public Task<FetchPurchaserGetResponseDTO> FetchPurchaserAsync(int purchaserId);

        public Task<FetchPurchaserAllPurchasesGetResponseDTO> FetchPurchaserAllPurchasesAsync(int purhcaserId, string sortBy = "id_asc", int pageNumber = 1, int pageSize = 10, string? searchString = null);

        public Task<AddPurchaserMachinePurchasePostResponseDTO> AddPurchaserMachinePurchaseAsync(int purchaserId, int machineId, int quantity);
    }
}
