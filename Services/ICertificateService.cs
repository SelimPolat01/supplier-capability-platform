using TedarikciKabiliyetYonetimSistemi.Entities;
using TedarikciKabiliyetYonetimSistemi.Models.DTO;

namespace TedarikciKabiliyetYonetimSistemi.Services
{
    public interface ICertificateService
    {
        public Task<AddCertificatePostResponseDTO> AddCertificateAsync(int userId, AddCertificatePostRequestDTO addCertificatePostRequestDTO);

        public Task<FetchAllCertificatesGetResponseDTO> FetchAllCertificatesAsync(int userId, int pageNumber = 1, int pageSize = 10, string sortBy = "id_asc", string? searchString = null);

        public Task<Certificate?> FetchCertificateAsync(int userId, int certificateId);

        public Task<EditCertificatePatchResponseDTO> EditCertificateAsync(EditCertificatePatchRequestDTO editCertificatePatchRequestDTO, int userId);

        public Task<RemoveCertificateDeleteResponseDTO> RemoveCertificateAsync(int userId, int certificateId);
    }
}
