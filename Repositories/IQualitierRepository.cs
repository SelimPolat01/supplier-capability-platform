using SupplierCapabilitiesAndManagementSystem.Entities;
using SupplierCapabilitiesAndManagementSystem.Enums;

namespace SupplierCapabilitiesAndManagementSystem.Repositories
{
    public interface IQualitierRepository
    {
        public Task<(List<AppUser> Data, int TotalCount)> FetchAllQualitiersAsync(string sortBy, int pageNumber, int pageSize, string? searchString = null);

        public Task<AppUser?> FetchQualitierAsync(int userId, int qualitierId);

        public Task<bool> EditSupplierMachineQualityAsync(int userId, int machineId, QualityScore? qualityScore);

        public Task<bool> EditSupplierCertificateQualityAsync(int userId, int certificateId, QualityScore? qualityScore);

        public Task<bool> EditSupplierHumanResourceQualityAsync(int userId, int humanResourceId, QualityScore? qualityScore);

        public Task<bool> EditSupplierQualityAsync(int userId, int supplierId, QualityScore? qualityScore);

        public Task<bool> EditPurchaserQualityAsync(int userId, int purchaserId, QualityScore? qualityScore);

        public Task<bool> EditPurchaserPurchaseQualityAsync(int userId, int purchaseId, QualityScore? qualityScore);
    }
}
