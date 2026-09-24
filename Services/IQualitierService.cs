using SupplierCapabilitiesAndManagementSystem.Enums;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;

namespace SupplierCapabilitiesAndManagementSystem.Services
{
    public interface IQualitierService
    {
        public Task<FetchAllQualitiersGetResponseDTO> FetchAllQualitiersAsync(string sortBy = "id_asc", int pageNumber = 1, int pageSize = 10, string? searchString = null);

        public Task<EditSupplierMachineQualityPatchResponseDTO> EditSupplierMachineQualityAsync(int userId, int machineId, QualityScore? qualityScore);

        public Task<EditSupplierCertificateQualityPatchResponseDTO> EditSupplierCertificateQualityAsync(int userId, int certificateId, QualityScore? qualityScore);

        public Task<EditSupplierHumanResourceQualityPatchResponseDTO> EditSupplierHumanResourceQualityAsync(int userId, int humanResourceId, QualityScore? qualityScore);

        public Task<EditSupplierQualityPatchResponseDTO> EditSupplierQualityAsync(int userId, int supplierId, QualityScore? qualityScore);

        public Task<EditPurchaserQualityPatchResponseDTO> EditPurchaserQualityAsync(int userId, int purchaserId, QualityScore? qualityScore);

        public Task<EditPurchaserPurchaseQualityPatchResponseDTO> EditPurchaserPurchaseQualityAsync(int userId, int purchaseId, QualityScore? qualityScore);
    }
}
