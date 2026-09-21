using SupplierCapabilitiesAndManagementSystem.Entities;

namespace SupplierCapabilitiesAndManagementSystem.Repositories
{
    public interface ISupplierCertificateRepository
    {
        public Task<SupplierCertificate> AddCertificateAsync(SupplierCertificate certificate);

        public Task<(List<SupplierCertificate> Data, int TotalCount)> FetchAllCertificatesAsync(int userId, int pageNumber, int pageSize, string sortBy, string? searchString = null);

        public Task<SupplierCertificate?> FetchCertificateAsync(int userId, int certificateId);

        public Task<SupplierCertificate?> EditCertificateAsync(SupplierCertificate certificate, int userId);

        public Task<bool> RemoveCertificateAsync(int userId, int certificateId);
    }
}
