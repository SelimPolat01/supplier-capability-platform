using TedarikciKabiliyetYonetimSistemi.Entities;

namespace TedarikciKabiliyetYonetimSistemi.Repositories
{
    public interface ICertificateRepository
    {
        public Task<Certificate> AddCertificateAsync(Certificate certificate);

        public Task<(List<Certificate> Data, int TotalCount)> FetchAllCertificatesAsync(int userId, int pageNumber, int pageSize, string sortBy, string? searchString = null);

        public Task<Certificate?> FetchCertificateAsync(int userId, int certificateId);

        public Task<Certificate?> EditCertificateAsync(Certificate certificate, int userId);

        public Task<bool> RemoveCertificateAsync(int userId, int certificateId);
    }
}
