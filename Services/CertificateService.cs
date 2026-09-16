using TedarikciKabiliyetYonetimSistemi.Entities;
using TedarikciKabiliyetYonetimSistemi.Models.DTO;
using TedarikciKabiliyetYonetimSistemi.Repositories;

namespace TedarikciKabiliyetYonetimSistemi.Services
{
    public class AddCertificateService : ICertificateService
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IWebHostEnvironment _env;

        public AddCertificateService(ICertificateRepository certificateRepository, IWebHostEnvironment env)
        {
            _certificateRepository = certificateRepository;
            _env = env;
        }
        public async Task<AddCertificatePostResponseDTO> AddCertificateAsync(int userId, AddCertificatePostRequestDTO addCertificatePostRequestDTO)
        {
            string fileUrl = "";

            if (addCertificatePostRequestDTO.CertificateFile != null && addCertificatePostRequestDTO.CertificateFile.Length > 0)
            {
                string[] permittedExtensions = { ".pdf", ".jpg", ".jpeg", ".png" };
                string fileExtension = Path.GetExtension(addCertificatePostRequestDTO.CertificateFile.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(fileExtension) || !permittedExtensions.Contains(fileExtension))
                {
                    return new AddCertificatePostResponseDTO { Message = "Invalid file type.", IsSuccess = false };
                }

                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                string uploadFolder = Path.Combine(_env.WebRootPath, "uploads");

                if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await addCertificatePostRequestDTO.CertificateFile.CopyToAsync(fileStream);
                }

                fileUrl = "/uploads/" + uniqueFileName;
            }
            else
            {
                return new AddCertificatePostResponseDTO { Message = "Certificate file is required.", IsSuccess = false };
            }

            try
            {
                Certificate certificate = addCertificatePostRequestDTO.ToCertificate(userId, fileUrl);
                Certificate addedCertificate = await _certificateRepository.AddCertificateAsync(certificate);

                return addedCertificate.ToAddCertificatePostResponseDTO("Certificate added successfully.", true);
            }
            catch (Exception ex)
            {
                return new AddCertificatePostResponseDTO()
                {
                    Message = $"An error occurred: {ex.Message}",
                    IsSuccess = false,
                };
            }
        }
        public async Task<FetchAllCertificatesGetResponseDTO> FetchAllCertificatesAsync(int userId, int pageNumber = 1, int pageSize = 10, string sortBy = "id_asc", string? searchString = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            try
            {
                (List<Certificate> Data, int TotalCount) result = await _certificateRepository.FetchAllCertificatesAsync(userId, pageNumber, pageSize, sortBy, searchString);
                int totalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize);

                return new FetchAllCertificatesGetResponseDTO()
                {
                    Data = result.Data,
                    TotalPages = totalPages,
                    CurrentPage = pageNumber,
                    Message = "Certificates retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new FetchAllCertificatesGetResponseDTO()
                {
                    Message = $"An error occurred while retrieving the certificates: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<Certificate?> FetchCertificateAsync(int userId, int certificateId)
        {
            return await _certificateRepository.FetchCertificateAsync(userId, certificateId);
        }

        public async Task<EditCertificatePatchResponseDTO> EditCertificateAsync(EditCertificatePatchRequestDTO editCertificatePatchRequestDTO, int userId)
        {
            Certificate certificate = editCertificatePatchRequestDTO.ToCertificate(userId);
            Certificate? updatedCertificate = await _certificateRepository.EditCertificateAsync(certificate, userId);

            if (updatedCertificate == null) return new EditCertificatePatchResponseDTO() { Message = "Certificate not found or unauthorized operation.", IsSuccess = false };

            return new EditCertificatePatchResponseDTO()
            {
                Message = "The certificate has been successfully updated.",
                IsSuccess = true,
                EditedCertificateId = updatedCertificate.Id
            };
        }

        public async Task<RemoveCertificateDeleteResponseDTO> RemoveCertificateAsync(int userId, int certificateId)
        {
            bool certificateIsDeleted = await _certificateRepository.RemoveCertificateAsync(userId, certificateId);

            if (certificateIsDeleted) return new RemoveCertificateDeleteResponseDTO() { Message = "The certificate was successfully deleted.", IsSuccess = true, DeletedCertificateId = certificateId };
            else return new RemoveCertificateDeleteResponseDTO() { Message = "The certificate could not be deleted.", IsSuccess = false };
        }
    }
}
