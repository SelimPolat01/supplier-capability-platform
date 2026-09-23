using SupplierCapabilitiesAndManagementSystem.Entities;
using SupplierCapabilitiesAndManagementSystem.Enums;

namespace SupplierCapabilitiesAndManagementSystem.Repositories
{
    public interface IQualitierRepository
    {
        public Task<(List<AppUser> Data, int TotalCount)> FetchAllQualitiersAsync(string sortBy, int pageNumber, int pageSize, string? searchString = null);

        public Task<bool> EditSupplierMachineQualityAsync(int userId, int machineId, QualityScore? qualityScore);

        public Task<bool> EditSupplierCertificateQualityAsync(int userId, int certificateId, QualityScore? qualityScore);
    }
}
