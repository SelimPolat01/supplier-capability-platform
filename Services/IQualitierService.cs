using SupplierCapabilitiesAndManagementSystem.Enums;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;

namespace SupplierCapabilitiesAndManagementSystem.Services
{
    public interface IQualitierService
    {
        public Task<FetchAllQualitiersGetResponseDTO> FetchAllQualitiersAsync(string sortBy = "id_asc", int pageNumber = 1, int pageSize = 10, string? searchString = null);

        public Task<EditSupplierMachineQualityPatchResponseDTO> EditSupplierMachineQualityAsync(int userId, int machineId, QualityScore? qualityScore);

        public Task<EditSupplierCertificateQualityPatchResponseDTO> EditSupplierCertificateQualityAsync(int userId, int certificateId, QualityScore? qualityScore);
    }
}
