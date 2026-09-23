using SupplierCapabilitiesAndManagementSystem.Entities;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Repositories;

namespace SupplierCapabilitiesAndManagementSystem.Services
{
    public class AddCertificateService : ISupplierCertificateService
    {
        private readonly ISupplierCertificateRepository _supplierCertificateRepository;
        private readonly IWebHostEnvironment _env;

        public AddCertificateService(ISupplierCertificateRepository certificateRepository, IWebHostEnvironment env)
        {
            _supplierCertificateRepository = certificateRepository;
            _env = env;
        }

        public async Task<FetchAllSuppliersCertificatesGetResponseDTO> FetchAllSuppliersCertificatesAsync(int pageNumber = 1, int pageSize = 10, string sortBy = "id_asc", string? searchString = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            try
            {
                (List<SupplierCertificate> Data, int TotalCount) result = await _supplierCertificateRepository.FetchAllSuppliersCertificatesAsync(pageNumber, pageSize, sortBy, searchString);

                int totalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize);
                var mappedData = result.Data.Select(supplierCertificate => supplierCertificate.ToSupplierCertificateDTO()).ToList();

                return new FetchAllSuppliersCertificatesGetResponseDTO()
                {
                    Data = mappedData,
                    TotalCount = result.TotalCount,
                    TotalPages = totalPages,
                    CurrentPage = pageNumber,
                    Message = "All suppliers certificates retrieved successfully.",
                    IsSuccess = true
                };

            }
            catch (Exception ex)
            {
                return new FetchAllSuppliersCertificatesGetResponseDTO()
                {
                    Message = $"An error occurred while retrieving the suppliers certificates: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<FetchSupplierAllCertificatesGetResponseDTO> FetchAllCertificatesAsync(int userId, int pageNumber = 1, int pageSize = 10, string sortBy = "id_asc", string? searchString = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            try
            {
                (List<SupplierCertificate> Data, int TotalCount) result = await _supplierCertificateRepository.FetchAllCertificatesAsync(userId, pageNumber, pageSize, sortBy, searchString);
                int totalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize);
                var mappedData = result.Data.Select(supplierCertificate => supplierCertificate.ToSupplierCertificateDTO()).ToList();

                return new FetchSupplierAllCertificatesGetResponseDTO()
                {
                    Data = mappedData,
                    TotalCount = result.TotalCount,
                    TotalPages = totalPages,
                    CurrentPage = pageNumber,
                    Message = "Certificates retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new FetchSupplierAllCertificatesGetResponseDTO()
                {
                    Message = $"An error occurred while retrieving the supplier certificates: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<FetchSupplierCertificateGetResponseDTO> FetchCertificateAsync(int userId, int certificateId)
        {
            try
            {
                SupplierCertificate? existingSupplierCertificate = await _supplierCertificateRepository.FetchCertificateAsync(userId, certificateId);

                if (existingSupplierCertificate == null)
                {
                    return new FetchSupplierCertificateGetResponseDTO()
                    {
                        Message = "Supplier certificate not found.",
                        IsSuccess = false
                    };
                }

                return new FetchSupplierCertificateGetResponseDTO()
                {
                    Certificate = existingSupplierCertificate.ToSupplierCertificateDTO(),
                    Message = "Certificate retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new FetchSupplierCertificateGetResponseDTO()
                {
                    Message = $"An error occurred while retrieving the certificate: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<AddSupplierCertificatePostResponseDTO> AddCertificateAsync(int userId, AddSupplierCertificatePostRequestDTO addSupplierCertificatePostRequestDTO)
        {
            string fileUrl = "";

            if (addSupplierCertificatePostRequestDTO.CertificateFile != null && addSupplierCertificatePostRequestDTO.CertificateFile.Length > 0)
            {
                string[] permittedExtensions = { ".pdf", ".jpg", ".jpeg", ".png" };
                string fileExtension = Path.GetExtension(addSupplierCertificatePostRequestDTO.CertificateFile.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(fileExtension) || !permittedExtensions.Contains(fileExtension))
                {
                    return new AddSupplierCertificatePostResponseDTO { Message = "Invalid file type.", IsSuccess = false };
                }

                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                string uploadFolder = Path.Combine(_env.WebRootPath, "uploads");

                if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await addSupplierCertificatePostRequestDTO.CertificateFile.CopyToAsync(fileStream);
                }

                fileUrl = "/uploads/" + uniqueFileName;
            }
            else
            {
                return new AddSupplierCertificatePostResponseDTO { Message = "Supplier certificate file is required.", IsSuccess = false };
            }

            try
            {
                SupplierCertificate supplierCertificate = addSupplierCertificatePostRequestDTO.ToSupplierCertificate(userId, fileUrl);
                SupplierCertificate addedSupplierCertificate = await _supplierCertificateRepository.AddCertificateAsync(supplierCertificate);

                return addedSupplierCertificate.ToAddSupplierCertificatePostResponseDTO("Supplier certificate added successfully.", true);
            }
            catch (Exception ex)
            {
                return new AddSupplierCertificatePostResponseDTO()
                {
                    Message = $"An error occurred: {ex.Message}",
                    IsSuccess = false,
                };
            }
        }

        public async Task<EditSupplierCertificatePatchResponseDTO> EditCertificateAsync(EditSupplierCertificatePatchRequestDTO editSupplierCertificatePatchRequestDTO, int userId)
        {
            SupplierCertificate supplierCertificate = editSupplierCertificatePatchRequestDTO.ToSupplierCertificate(userId);
            SupplierCertificate? updatedSupplierCertificate = await _supplierCertificateRepository.EditCertificateAsync(supplierCertificate, userId);

            if (updatedSupplierCertificate == null) return new EditSupplierCertificatePatchResponseDTO() { Message = "Supplier certificate not found or unauthorized operation.", IsSuccess = false };

            return new EditSupplierCertificatePatchResponseDTO()
            {
                Message = "The supplier certificate has been successfully updated.",
                IsSuccess = true,
                EditedCertificateId = updatedSupplierCertificate.Id
            };
        }

        public async Task<RemoveSupplierCertificateDeleteResponseDTO> RemoveCertificateAsync(int userId, int supplierCertificateId)
        {
            bool suuplierCertificateIsDeleted = await _supplierCertificateRepository.RemoveCertificateAsync(userId, supplierCertificateId);

            if (suuplierCertificateIsDeleted) return new RemoveSupplierCertificateDeleteResponseDTO() { Message = "The supplier certificate was successfully deleted.", IsSuccess = true, DeletedCertificateId = supplierCertificateId };
            else return new RemoveSupplierCertificateDeleteResponseDTO() { Message = "The supplier certificate could not be deleted.", IsSuccess = false };
        }
    }
}
