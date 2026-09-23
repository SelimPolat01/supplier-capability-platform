using SupplierCapabilitiesAndManagementSystem.Entities;

namespace SupplierCapabilitiesAndManagementSystem.Repositories
{
    public interface IPurchaserRepository
    {
        public Task<(List<AppUser> Data, int TotalCount)> FetchAllPurchasersAsync(string sortBy, int pageNumber, int pageSize, string? searchString = null);

        public Task<AppUser?> FetchPurchaserAsync(int purchaserId);

        public Task<(List<PurchaserMachinePurchase> Data, int TotalCount)> FetchPurchasersAllPurchaseAsync(int purchaserId, string sortBy, int pageNumber, int pageSize, string? searchString = null);

        public Task<PurchaserMachinePurchase?> FetchPurchaserPurchaseAsync(int purchaseId);

        public Task<(int PurchaseId, string ErrorMessage)> AddPurchaserMachinePurchaseAsync(int purchaserId, int machineId, int quantity = 1);
    }
}
