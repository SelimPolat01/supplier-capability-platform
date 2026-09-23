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
    }
}
