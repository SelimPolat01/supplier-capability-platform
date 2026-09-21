using SupplierCapabilitiesAndManagementSystem.Entities;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;

namespace SupplierCapabilitiesAndManagementSystem.Services
{
    public interface ISupplierCertificateService
    {
        public Task<AddSupplierCertificatePostResponseDTO> AddCertificateAsync(int userId, AddSupplierCertificatePostRequestDTO addCertificatePostRequestDTO);

        public Task<FetchSupplierAllCertificatesGetResponseDTO> FetchAllCertificatesAsync(int userId, int pageNumber = 1, int pageSize = 10, string sortBy = "id_asc", string? searchString = null);

        public Task<SupplierCertificate?> FetchCertificateAsync(int userId, int certificateId);

        public Task<EditSupplierCertificatePatchResponseDTO> EditCertificateAsync(EditSupplierCertificatePatchRequestDTO editCertificatePatchRequestDTO, int userId);

        public Task<RemoveSupplierCertificateDeleteResponseDTO> RemoveCertificateAsync(int userId, int certificateId);
    }
}
