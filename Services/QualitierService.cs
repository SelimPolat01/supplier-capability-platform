using SupplierCapabilitiesAndManagementSystem.Entities;
using SupplierCapabilitiesAndManagementSystem.Enums;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Repositories;

namespace SupplierCapabilitiesAndManagementSystem.Services
{
    public class QualitierService : IQualitierService
    {
        private readonly IQualitierRepository _qualitierRepository;

        public QualitierService(IQualitierRepository qualitierRepository)
        {
            _qualitierRepository = qualitierRepository;
        }

        public async Task<FetchAllQualitiersGetResponseDTO> FetchAllQualitiersAsync(string sortBy = "id_asc", int pageNumber = 1, int pageSize = 10, string? searchString = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            try
            {
                (List<AppUser> Data, int TotalCount) result = await _qualitierRepository.FetchAllQualitiersAsync(sortBy, pageNumber, pageSize, searchString);
                int totalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize);
                var mappedData = result.Data.Select(qualitier => qualitier.ToQualitierDTO()).ToList();

                return new FetchAllQualitiersGetResponseDTO()
                {
                    Data = mappedData,
                    TotalPages = totalPages,
                    TotalCount = result.TotalCount,
                    CurrentPage = pageNumber,
                    Message = "Qualitiers retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new FetchAllQualitiersGetResponseDTO()
                {
                    Message = $"An error occurred while retrieving the qualitiers: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<FetchQualitierGetResponseDTO> FetchQualitierAsync(int userId, int qualitierId)
        {
            try
            {
                AppUser? existingQualitier = await _qualitierRepository.FetchQualitierAsync(userId, qualitierId);

                if (existingQualitier == null)
                {
                    return new FetchQualitierGetResponseDTO()
                    {
                        Message = "Qualitier profile not found, or you do not have admin privileges to view this page.",
                        IsSuccess = false
                    };
                }

                return new FetchQualitierGetResponseDTO()
                {
                    Qualitier = existingQualitier.ToQualitierDTO(),
                    Message = "Qualitier details retrieved successfully.",
                    IsSuccess = true
                };

            }
            catch (Exception ex)
            {
                return new FetchQualitierGetResponseDTO()
                {
                    Message = $"An error occurred while retrieving the qualitier: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<EditSupplierMachineQualityPatchResponseDTO> EditSupplierMachineQualityAsync(int userId, int machineId, QualityScore? qualityScore)
        {
            if (machineId <= 0)
            {
                return new EditSupplierMachineQualityPatchResponseDTO()
                {
                    Message = "Invalid machine ID.",
                    IsSuccess = false
                };
            }

            if (qualityScore == null)
            {
                return new EditSupplierMachineQualityPatchResponseDTO()
                {
                    Message = "Quality score cannot be empty.",
                    IsSuccess = false
                };
            }

            try
            {
                bool result = await _qualitierRepository.EditSupplierMachineQualityAsync(userId, machineId, qualityScore);

                if (!result)
                {
                    return new EditSupplierMachineQualityPatchResponseDTO()
                    {
                        Message = "Failed to update the machine's quality score. The machine might not exist or the user is not authorized.",
                        IsSuccess = false
                    };
                }

                return new EditSupplierMachineQualityPatchResponseDTO()
                {
                    Message = "Machine quality score updated successfully.",
                    IsSuccess = true,
                    EditedMachineId = machineId
                };
            }
            catch (Exception ex)
            {
                return new EditSupplierMachineQualityPatchResponseDTO()
                {
                    Message = $"An error occurred while updating the quality score: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<EditSupplierCertificateQualityPatchResponseDTO> EditSupplierCertificateQualityAsync(int userId, int certificateId, QualityScore? qualityScore)
        {
            if (certificateId <= 0)
            {
                return new EditSupplierCertificateQualityPatchResponseDTO()
                {
                    Message = "Invalid certificate ID.",
                    IsSuccess = false
                };
            }

            if (qualityScore == null)
            {
                return new EditSupplierCertificateQualityPatchResponseDTO()
                {
                    Message = "Quality score cannot be empty.",
                    IsSuccess = false
                };
            }

            try
            {
                bool result = await _qualitierRepository.EditSupplierCertificateQualityAsync(userId, certificateId, qualityScore);

                if (!result)
                {
                    return new EditSupplierCertificateQualityPatchResponseDTO()
                    {
                        Message = "Failed to update the certificate's quality score. The certificate might not exist or the user is not authorized.",
                        IsSuccess = false
                    };
                }

                return new EditSupplierCertificateQualityPatchResponseDTO()
                {
                    Message = "Certificate quality score updated successfully.",
                    IsSuccess = true,
                    EditedCertificateId = certificateId
                };
            }
            catch (Exception ex)
            {
                return new EditSupplierCertificateQualityPatchResponseDTO()
                {
                    Message = $"An error occurred while updating the quality score: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<EditSupplierHumanResourceQualityPatchResponseDTO> EditSupplierHumanResourceQualityAsync(int userId, int humanResourceId, QualityScore? qualityScore)
        {
            if (humanResourceId <= 0)
            {
                return new EditSupplierHumanResourceQualityPatchResponseDTO()
                {
                    Message = "Invalid human resource ID.",
                    IsSuccess = false
                };
            }

            if (qualityScore == null)
            {
                return new EditSupplierHumanResourceQualityPatchResponseDTO()
                {
                    Message = "Quality score cannot be empty.",
                    IsSuccess = false
                };
            }

            try
            {
                bool result = await _qualitierRepository.EditSupplierHumanResourceQualityAsync(userId, humanResourceId, qualityScore);

                if (!result)
                {
                    return new EditSupplierHumanResourceQualityPatchResponseDTO()
                    {
                        Message = "Failed to update the human resource's quality score. The human resource might not exist or the user is not authorized.",
                        IsSuccess = false
                    };
                }

                return new EditSupplierHumanResourceQualityPatchResponseDTO()
                {
                    Message = "Human resource quality score updated successfully.",
                    IsSuccess = true,
                    EditedHumanResourceId = humanResourceId
                };
            }
            catch (Exception ex)
            {
                return new EditSupplierHumanResourceQualityPatchResponseDTO()
                {
                    Message = $"An error occurred while updating the quality score: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<EditSupplierQualityPatchResponseDTO> EditSupplierQualityAsync(int userId, int supplierId, QualityScore? qualityScore)
        {
            if (supplierId <= 0)
            {
                return new EditSupplierQualityPatchResponseDTO()
                {
                    Message = "Invalid supplier ID.",
                    IsSuccess = false
                };
            }

            if (qualityScore == null)
            {
                return new EditSupplierQualityPatchResponseDTO()
                {
                    Message = "Quality score cannot be empty.",
                    IsSuccess = false
                };
            }

            try
            {
                bool result = await _qualitierRepository.EditSupplierQualityAsync(userId, supplierId, qualityScore);

                if (!result)
                {
                    return new EditSupplierQualityPatchResponseDTO()
                    {
                        Message = "Failed to update the supplier's quality score. The supplier might not exist or the user is not authorized.",
                        IsSuccess = false
                    };
                }

                return new EditSupplierQualityPatchResponseDTO()
                {
                    Message = "Supplier quality score updated successfully.",
                    IsSuccess = true,
                    EditedSupplierId = supplierId
                };
            }
            catch (Exception ex)
            {
                return new EditSupplierQualityPatchResponseDTO()
                {
                    Message = $"An error occurred while updating the quality score: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<EditPurchaserQualityPatchResponseDTO> EditPurchaserQualityAsync(int userId, int purchaserId, QualityScore? qualityScore)
        {
            if (purchaserId <= 0)
            {
                return new EditPurchaserQualityPatchResponseDTO()
                {
                    Message = "Invalid purchaser ID.",
                    IsSuccess = false
                };
            }

            if (qualityScore == null)
            {
                return new EditPurchaserQualityPatchResponseDTO()
                {
                    Message = "Quality score cannot be empty.",
                    IsSuccess = false
                };
            }

            try
            {
                bool result = await _qualitierRepository.EditPurchaserQualityAsync(userId, purchaserId, qualityScore);

                if (!result)
                {
                    return new EditPurchaserQualityPatchResponseDTO()
                    {
                        Message = "Failed to update the purchaser's quality score. The purchaser might not exist or the user is not authorized.",
                        IsSuccess = false
                    };
                }

                return new EditPurchaserQualityPatchResponseDTO()
                {
                    Message = "Purchaser quality score updated successfully.",
                    IsSuccess = true,
                    EditedPurchaserId = purchaserId
                };
            }
            catch (Exception ex)
            {
                return new EditPurchaserQualityPatchResponseDTO()
                {
                    Message = $"An error occurred while updating the quality score: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<EditPurchaserPurchaseQualityPatchResponseDTO> EditPurchaserPurchaseQualityAsync(int userId, int purchaseId, QualityScore? qualityScore)
        {
            if (purchaseId <= 0)
            {
                return new EditPurchaserPurchaseQualityPatchResponseDTO()
                {
                    Message = "Invalid purchase ID.",
                    IsSuccess = false
                };
            }

            if (qualityScore == null)
            {
                return new EditPurchaserPurchaseQualityPatchResponseDTO()
                {
                    Message = "Quality score cannot be empty.",
                    IsSuccess = false
                };
            }

            try
            {
                bool result = await _qualitierRepository.EditPurchaserPurchaseQualityAsync(userId, purchaseId, qualityScore);

                if (!result)
                {
                    return new EditPurchaserPurchaseQualityPatchResponseDTO()
                    {
                        Message = "Failed to update the purchase's quality score. The purchase might not exist or the user is not authorized.",
                        IsSuccess = false
                    };
                }

                return new EditPurchaserPurchaseQualityPatchResponseDTO()
                {
                    Message = "Purchase quality score updated successfully.",
                    IsSuccess = true,
                    EditedPurchaserPurchaseId = purchaseId
                };
            }
            catch (Exception ex)
            {
                return new EditPurchaserPurchaseQualityPatchResponseDTO()
                {
                    Message = $"An error occurred while updating the quality score: {ex.Message}",
                    IsSuccess = false
                };
            }
        }
    }
}
